using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Text;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using IBSLFormsApp.Model.Libraries;
using static IBSLFormsApp.Model.Payment.Payment_Period_Seqs;
using IBSLFormsApp.Model.MapDataPayment;
using IBSLFormsApp.Helpers;

namespace IBSLFormsApp.Model.Payment
{
    public class PaymentProgram
    {
        string startFolderPath = Application.StartupPath;

        string filename = "\\log\\Ibsl_payment_";

        string key_channel = "";

        /*************** Log Test ***************/
        
        DebugLog debl = new DebugLog();
        //string logTest = "**********";

        public async Task<String> Paymentprogram(string storeProc ,string servername,string usrName, string pwd)
        {
            dynamic restxt = null;

            string urlPostRequest = "";

            var timestamp = DateTime.Now.ToFileTime();


            var reqTokenAPI = new ReqestTokenApi();

            MapPayModel logModel = new MapPayModel();

            string ResponsePostForToken = await reqTokenAPI.requesttokenapi(servername,usrName,pwd);

            JObject TokenJson = JObject.Parse(ResponsePostForToken);
            string access_token = TokenJson["token_type"].ToString() + " " + TokenJson["access_token"].ToString();
            //Console.WriteLine("TokenJson : " + TokenJson["access_token"]);
            //Console.WriteLine(TokenJson.ToString());

            if (servername == "uat")
            {
                urlPostRequest = "https://ibsl-uat-api.oic.or.th";
                key_channel = "ibsl_UAT.crt";
                filename = "\\log\\UAT_Ibsl_payment_";
            }
            else
            {
                urlPostRequest = "https://ibsl-api.oic.or.th";
                key_channel = "ibsl_PRD.crt";
                filename = "\\log\\PRD_Ibsl_payment_";
            }

            string exportfile = startFolderPath + filename + timestamp + ".txt";

            if (!File.Exists(exportfile))
            {
                File.WriteAllText(exportfile, TokenJson.ToString() + Environment.NewLine);
                //File.WriteAllText(exportfile, goutjson.Substring(1) + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(exportfile, TokenJson.ToString() + Environment.NewLine);
                //File.AppendAllText(exportfile, goutjson.Substring(1) + Environment.NewLine);
            }

            IbslUatPaymentApi ibsluatapi = new IbslUatPaymentApi();

            //Console.WriteLine("Test: " + jsonarry.ToString());

            PaymentSensitiveFields sensitive = new PaymentSensitiveFields();

            var sensFil = sensitive.PaymentSensFields();

            List<string> sensitiveFieldList = new List<string>();

            foreach (var vl in sensFil)
            {
                sensitiveFieldList.Add(vl.ToString());
            }

            //bool alreadyExist = sensitiveFieldList.Contains("pol_payment_period");

            //Console.WriteLine("List: " + alreadyExist.ToString());

            try
            {
                //string connectionString = "Data Source=10.20.25.101;Initial Catalog=IBS_Life;User ID=devconnect;Password=P@ssw0rd1234";
                String connectionString = "Data Source=VELA\\SQLEXPRESS;Initial Catalog=IBS_Life;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(storeProc, connection))
                    {
                        connection.Open();
                        var jsonResult = new StringBuilder();
                        var reader = cmd.ExecuteReader();

                        //Console.WriteLine("From db reader:> "+reader.FieldCount +"\n");

                        var Result_json = "";
                        var ResultSet = new List<string>();

                        if (!reader.HasRows)
                        {
                            jsonResult.Append("[]");
                            //Result_json = jsonResult.ToString();
                            Console.WriteLine("Has Rows:" + Result_json + "\n");
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                jsonResult.Append(reader.GetValue(0).ToString());
                                //Console.WriteLine("Read each: " + reader.GetValue(0) + "\n");
                                //Result_json = Result_json + jsonResult.ToString();
                                /*----------------------------------------------*/
                            }
                            dynamic jsonArray = JsonConvert.DeserializeObject(jsonResult.ToString());

                            //debl.debuglog("jsonArray: "+ jsonArray);
                            //Console.WriteLine("**********************************************************");
                            //Console.WriteLine("Policy Json: " + jsonArray.ToString() + "\n");

                            string gotOutput = "";
                            string goutjson = "";
                            string ref_seq = "";
                            string pol_id = "";
                            string send_res_code = "";
                            string send_res_msg = "";

                            string tranc_status = "";
                            string send_tranc_status = "";
                            string tranc_res = "";

                            foreach (var item in jsonArray)
                            {
                                //Console.WriteLine("ITEMS := " + item + "\n");

                                ref_seq = item["pmt_refer_code_of_company"];

                                tranc_status = item["pmt_transaction_status"];

                                var properties = item.Properties();

                                //Console.WriteLine("Properties :> " + properties.Tostring() + "\n");

                                string playloadResult = EncyProc(sensitiveFieldList, item, key_channel);

                                dynamic parsedJson = JsonConvert.DeserializeObject(playloadResult);

                                //string response = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);

                                //--- for export to log file ---//

                                //string response = await ibsluatapi.ibsuatpaymentapi(access_token, urlPostRequest, parsedJson);
                                var response = Task.Run(async () => await ibsluatapi.ibsuatpaymentapi(access_token, urlPostRequest, parsedJson)).Result;

                                restxt = JsonConvert.DeserializeObject(response);

                                #region Export to Log
                                logModel.Body = parsedJson;
                                logModel.Response = restxt;
                                #endregion

                                //**************************  for indent format in log file ******************//
                                string logResTxt2 = JsonConvert.SerializeObject(logModel, Formatting.Indented);

                                //**************************  one record one line in log file ******************//
                                //string logResTxt2 = JsonConvert.SerializeObject(logModel);

                                Console.WriteLine("outputAll: " + logResTxt2);

                                if (!File.Exists(exportfile))
                                {
                                    File.WriteAllText(exportfile, logResTxt2 + Environment.NewLine);
                                    //File.WriteAllText(exportfile, goutjson.Substring(1) + Environment.NewLine);
                                }
                                else
                                {
                                    File.AppendAllText(exportfile, logResTxt2 + Environment.NewLine);
                                    //File.AppendAllText(exportfile, goutjson.Substring(1) + Environment.NewLine);
                                }

                                JObject obj = JObject.Parse(response);

                                //debl.debuglog("response: " + obj["response_id"] + " / " + obj["errors"]);

                                string response_id = obj["response_id"].ToString();
                                string res_error = "";
                                string record_status = "";

                                try
                                {
                                    if (obj["errors"] != null)
                                    {
                                        res_error = obj["errors"][0].ToString();
                                        //debl.debuglog("reserror1: " + res_error);
                                        JObject getMessageJobj = JObject.Parse(res_error);
                                        send_res_code = getMessageJobj["code"].ToString();
                                        send_res_msg = getMessageJobj["message"].ToString();
                                        //debl.debuglog("ref_seq: " + ref_seq + " message:" + getMessageJobj["message"]);

                                        if (send_res_code == "1000001") //**** Duplicate *****//
                                        {
                                            record_status = "U";
                                        }
                                        else if (record_status == "N")
                                        {
                                            record_status = "N";
                                        }

                                        send_tranc_status = "ER";
                                        tranc_res = "res_id:" + response_id;
                                    }
                                    else
                                    {
                                        res_error = "Success";
                                        record_status = "S";
                                        send_tranc_status = "SC";
                                        tranc_res = "res_id:" + response_id;
                                    }
                                }
                                catch
                                (Exception ex)
                                {
                                    debl.debuglog("Err Response: " + ex.Message);
                                }


                                //debl.debuglog("ref_seq2: " + ref_seq + " > response: " + send_tranc_status + " / " + tranc_res);

                                //********************** UPDATE API REQUEST STATUS **********************//
                                try
                                {
                                    using (SqlConnection conn = new SqlConnection(connectionString))
                                    {
                                        string upresp_status = " UPDATE Temp_Payment_IBSL SET pmt_transaction_status = '" + record_status + "' ,send_status = '" + send_tranc_status + "', send_res = '" + tranc_res + "' " +

                                            " ,send_res_code = '" + send_res_code + "', send_res_msg='" + send_res_msg + "'" +

                                            " WHERE pmt_refer_code_of_company = '" + ref_seq + "'";
                                        
                                        //debl.debuglog("Update Sql: " + upresp_status);

                                        using (SqlCommand updateStatusCommand = new SqlCommand(upresp_status, conn))
                                        {
                                            conn.Open();
                                            //await debl.debuglog("xxxxxxxx");
                                            updateStatusCommand.ExecuteNonQuery();
                                            conn.Close();
                                        };
                                        //debl.debuglog("query: " + upresp_status);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    debl.debuglog("Error Update SQL: " + ex.ToString());
                                }
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                debl.debuglog("Err Main: " + ex.Message);
            }

            return restxt.ToString();

        }

        private static string EncyProc(List<string> sensitiveFieldList, JObject jsonStringFormat, string key_channel)
        {
            string startFolderPath = Application.StartupPath;

            string getEncryp = "";

            string privKeyCentralPath = startFolderPath + "\\SSLKey\\ibsl.key";

            string pubKeyCentralPath = startFolderPath + "\\SSLKey\\" + key_channel;

            string privKeycompPath = startFolderPath + "\\SSLKey\\company.key";

            string pubKeycompPath = startFolderPath + "\\SSLKey\\company.crt";

            //********************** Set colume URL [] ********************//
            List<string> listUrl = new List<string>();
            listUrl.Add("pol_document_urls");
            //-------------------------------------------------------------//

            #region JsonSerializerSettings
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.Formatting = Formatting.Indented;
            #endregion

            #region Prepare Data

            //Console.WriteLine("In Json: " + jsonStringFormat);
            string pocStr = JsonConvert.SerializeObject(jsonStringFormat);
            //Console.WriteLine("-------------------------------------");
            //Console.WriteLine("posStr : " + pocStr);

            PaymentModel poc = JsonConvert.DeserializeObject<PaymentModel>(pocStr);

            //Console.WriteLine("PRE DATA => ");
            //Console.WriteLine(poc);
            //Console.WriteLine(JsonConvert.SerializeObject(poc, serializerSettings));
            // Console.WriteLine("-------------------------------------\n");
            #endregion


            //------- Encrypt data ---------
            //------- Encrypt data ---------
            string inputValue;
            string encryptText = "";
            object jsonarry = "";
            //string value = "";
            //var attr = "";

            try
            {
                foreach (var prop in poc.GetType().GetProperties())
                {
                    var attr = poc.GetType().GetProperty(prop.Name);
                    var value = attr.GetValue(poc, null);

                    /**************************** Switch check More JsonObject *******************************/
                    /**try
                    {**/
                    switch (prop.Name)
                    {
                        case "payment_period_seqs":
                            //Console.WriteLine("Customer value: " + value.ToString());
                            //Console.WriteLine("Check Type : Json Object \n");
                            if (value != null)
                            {
                                string ResultJObject = payment_period_seqs(sensitiveFieldList, value, pubKeyCentralPath);
                                try
                                {
                                    dynamic parsedJson = JsonConvert.DeserializeObject(ResultJObject);
                                    string encrypeOut = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                                    //Console.WriteLine("Main result: "+ encrypeOut + "\n");
                                    JArray json = JArray.Parse(encrypeOut);
                                    //Console.WriteLine("Aray result: "+ json + "\n");
                                    attr.SetValue(poc, json);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error attr.SetValue: " + ex);
                                }
                            }
                            else
                            {
                                Array valay = new Array[] { };
                                var DumArray = JArray.FromObject(valay);
                                //Console.WriteLine("valay: " + DumArray);
                                attr.SetValue(poc, DumArray);
                                //attr.SetValue(poc, value);
                            }
                            break;

                        default:

                            if (value == null)
                            {
                                attr.SetValue(poc, value);
                            }
                            else
                            {
                                bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                                //Console.WriteLine("payment: " + prop.Name + ": " + value);

                                if (alreadyExist)
                                {
                                    inputValue = value.ToString();
                                    encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                    attr.SetValue(poc, encryptText);
                                }
                                else
                                {
                                    attr.SetValue(poc, value);
                                }
                            }

                            break;
                    }
                    //----------- Check item name ----------//

                    /*  }
                      catch
                      {
                          Console.WriteLine("Error json policy !!!");
                      }
                    */

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " Encrypt => Can't find public-key ibsl. \n");
            }

            //------- Sign data ---------
            string preItem = JsonConvert.SerializeObject(poc);
            byte[] dataForSigning = BaseHelper.ConvertStringToByte(preItem);
            string signature = "";
            try
            {
                signature = Signing(dataForSigning, privKeycompPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " Signing => Can't find private-key company. \n");
            }

            #region Payload
            EditPaymentModel model = new EditPaymentModel();
            model.payload = poc;
            model.Signature = signature;
            //Console.WriteLine("PAYLOAD => ");
            //Console.WriteLine("----------------------------------");
            //Console.WriteLine(JsonConvert.SerializeObject(model, serializerSettings));
            //Console.WriteLine("----------------------------------\n");
            #endregion

            getEncryp = JsonConvert.SerializeObject(model);

            //------- Verify data ---------
            Console.WriteLine("------- VERIFY -------");
            string itemModel = JsonConvert.SerializeObject(model.payload);
            byte[] rawData = BaseHelper.ConvertStringToByte(itemModel);
            byte[] signData = Convert.FromBase64String(model.Signature);
            bool isValid = false;
            try
            {
                isValid = Verify(rawData, signData, pubKeycompPath);
                Console.WriteLine("IsValid : " + isValid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Verify => Can't find public-key company. \n");
            }

            //------- Decrypt data ---------
            if (isValid)
            {
                try
                {
                    string decryptText;

                    Console.WriteLine("\n------- DECRYPT -----------");
                    foreach (var prop in poc.GetType().GetProperties())
                    {
                        var attr = poc.GetType().GetProperty(prop.Name);
                        var value = attr.GetValue(poc, null);
                        try
                        {
                            if (value == "" || value is "[]")
                            {
                                decryptText = value == "" ? "" : value.ToString(); ;
                            }
                            else
                            {
                                //---inputValue = value.ToString();
                                //---decryptText = DecryptData(value.ToString(), privKeyCentralPath);
                                //---Console.WriteLine($"Decode: {decryptText}");
                            }
                            //string decryptText = DecryptData(value.ToString(), privKeyCentralPath);
                            #region
                            //--- comment disable decrypt decryptText ---//
                            //attr.SetValue(poc, decryptText);
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Decrypt => Can't find private-key ibsl. \n");
                }
            }
            else
            {
                Console.WriteLine("Signature is not valid. \n");
            }

            // Console.WriteLine(JsonConvert.SerializeObject(model, serializerSettings));
            //Console.WriteLine("-----------End Encrypt-------------\n");

            //***** Wait for the user to respond before closing.*****//
            //Console.WriteLine("Press r to restart");
            //ConsoleKeyInfo restart = Console.ReadKey();
            //Console.WriteLine();

            //await Main(null);
            return getEncryp;
        }

        //==================================================================================//
        private static string DecryptData(string value, string centralKeyPath)
        {
            string text = File.ReadAllText(centralKeyPath);
            string privateKey = text.Replace("-----BEGIN PRIVATE KEY-----", string.Empty)
                .Replace("-----END PRIVATE KEY-----", string.Empty);
            byte[] privateKeyBytes = Convert.FromBase64String(privateKey);
            RSA rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            try
            {
                byte[] data = Convert.FromBase64String(value);
                byte[] decryptData = rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
                string decryptText = BaseHelper.ConvertByteToString(decryptData);

                return decryptText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string Signing(byte[] rawData, string compKeyPath)
        {
            string text = File.ReadAllText(compKeyPath);
            string privateKey = text.Replace("-----BEGIN PRIVATE KEY-----", string.Empty)
                .Replace("-----END PRIVATE KEY-----", string.Empty);
            byte[] privateKeyBytes = Convert.FromBase64String(privateKey);
            RSA rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

            byte[] signData = rsa.SignData(rawData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            string signText = Convert.ToBase64String(signData);

            return signText;
        }
        private static bool Verify(byte[] rawData, byte[] signData, string compCertPath)
        {
            bool isValid = false;

            // Load the certificate into an X509Certificate object.
            X509Certificate cert = new X509Certificate(compCertPath);
            RSA rsa = RSA.Create();
            rsa.ImportRSAPublicKey(cert.GetPublicKey(), out _);

            isValid = rsa.VerifyData(rawData, signData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return isValid;
        }

    }
}
