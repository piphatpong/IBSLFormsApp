using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using IBSLFormsApp.Model.Libraries;
using static IBSLFormsApp.Model.Claim.Claim_coverages;
using static IBSLFormsApp.Model.Claim.ClaimPaymentPrg;
using static IBSLFormsApp.Model.Claim.EstimateProgram;
using static IBSLFormsApp.Model.Libraries.DateTimeConvert;
using IBSLFormsApp.Model.Claim;
using IBSLFormsApp.Model.MapDataClaim;
using System.Linq;
using IBSLFormsApp.Helpers;
using System.Text.Json.Nodes;

namespace IBSLFormsApp
{
    public class ClaimProgram
    {
        string startFolderPath = Application.StartupPath;

        string filename = "\\log\\Ibsl_claim_";

        string key_channel = "";

        /*************** Log Test ***************/
        //string logTest = "Count sql: " + reader.FieldCount + " store proc:" + storeProc;
        DebugLog debl = new DebugLog();
        string logTest = "";

        //const string filename = "D:\\SahaLife\\PkiDemo\\IBSLPortal\\IBSLFormsApp\\IBSLFormsApp\\log\\Ibsl_claim_";

        public async Task<String> claimprogram(string storeProc, string servername, string usrName, string pwd)
        {
            dynamic restxt = null;

            string urlPostRequest = "";

            var timestamp = DateTime.Now.ToFileTime();

            MapClaimModel logModel = new MapClaimModel();

            var reqTokenAPI = new ReqestTokenApi();

            string ResponsePostForToken = await reqTokenAPI.requesttokenapi(servername, usrName, pwd);

            JObject TokenJson = JObject.Parse(ResponsePostForToken);
            string access_token = TokenJson["token_type"].ToString()+ " "+ TokenJson["access_token"].ToString();
            //Console.WriteLine("TokenJson : " + TokenJson["access_token"]);

            if (servername == "uat")
            {
                urlPostRequest = "https://ibsl-uat-api.oic.or.th";
                key_channel = "ibsl_UAT.crt";
                filename = "\\log\\UAT_Ibsl_claim_";

            }
            else
            {
                urlPostRequest = "https://ibsl-api.oic.or.th";
                key_channel = "ibsl_PRD.crt";
                filename = "\\log\\PRD_Ibsl_claim_";
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

            PostPolAsync postPolrequest = new PostPolAsync();

            IbslUatClaimApi ibsluatapi = new IbslUatClaimApi();

            

            ClaimSensitiveFields sensitive = new ClaimSensitiveFields();
            var sensFil = sensitive.SensFields();
            List<string> sensitiveFieldList = new List<string>();
            foreach (var vl in sensFil)
            {
                sensitiveFieldList.Add(vl.ToString());
            }

            //bool alreadyExist = sensitiveFieldList.Contains("pol_payment_period");

            //Console.WriteLine("List: " + alreadyExist.ToString());
            
            try
            {   
                //String connectionString = "Data Source=10.20.25.101;Initial Catalog=IBS_Life;User ID=devconnect;Password=P@ssw0rd1234";
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
                        //debl.debuglog("sql command: " + reader.ToString());

                        if (!reader.HasRows)
                        {
                            restxt = "!!! No data from database";
                            jsonResult.Append("[]");
                            debl.debuglog("NO Has Rows");
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
                            //debl.debuglog("json query: " + jsonArray);
                            //Console.WriteLine("**********************************************************");
                            //Console.WriteLine("Claim Json: " + jsonArray.ToString() + "\n");

                            //debl.debuglog("json: available !!!");

                            string gotOutput = "";
                            string goutjson = "";
                            string ref_seq = "";
                            string pol_id = "";
                            string send_res_code = "";
                            string send_res_msg = "";

                            string tranc_status = "";
                            string send_tranc_status = "";
                            string tranc_res = "";

                            if (true)
                            {
                                foreach (var item in jsonArray)
                                {
                                    ref_seq = item["claim_refer_code_of_company"];
                                    tranc_status = item["claim_transaction_status"];

                                    string playloadResult = EncyProc(sensitiveFieldList, item, key_channel);

                                    dynamic parsedJson = JsonConvert.DeserializeObject(playloadResult);

                                    //string encrypeOut = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);

                                    //--- for export to log file ---//

                                    string response = await ibsluatapi.ibsuatclaimapi(access_token, urlPostRequest, parsedJson);

                                    restxt = JsonConvert.DeserializeObject(response);

                                    #region Export to Log
                                    logModel.Body = parsedJson;
                                    logModel.Response = restxt;
                                    #endregion

                                    //**************************  for indent format in log file ******************//
                                    string logResTxt2 = JsonConvert.SerializeObject(logModel, Formatting.Indented);



                                    //**************************  one record one line in log file ******************//
                                    //string logResTxt2 = JsonConvert.SerializeObject(logModel);

                                    //Console.WriteLine("outputAll: " + logResTxt2);

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
                                            else if (record_status == "N" ) {
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

                                    //********************** UPDATE API REQUEST STATUS **********************//
                                    try
                                    {
                                        using (SqlConnection conn = new SqlConnection(connectionString))
                                        {
                                            string upresp_status = " UPDATE Temp_Claim SET claim_transaction_status = '" + record_status + "' ,send_status = '" + send_tranc_status + "', send_res = '" + tranc_res + "' " +
                                                                        " ,send_res_code = '"+ send_res_code+"', send_res_msg='"+ send_res_msg  + "'" +
                                                                        " WHERE claim_refer_code_of_company = '" + ref_seq + "'";

                                            using (SqlCommand updateStatusCommand = new SqlCommand(upresp_status, conn))
                                            {
                                                conn.Open();
                                                //await debl.debuglog("xxxxxxxx");
                                                updateStatusCommand.ExecuteNonQuery();
                                                conn.Close();
                                            };
                                            //debl.debuglog("query: " + upresp_status);
                                        }
                                    } catch (Exception ex) { 
                                       debl.debuglog ("Error Update SQL: " + ex.ToString());
                                    }
                                }
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception: " + ex.ToString());
                debl.debuglog("Error Claim Main: " + ex.Message);
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
            //List<string> listUrl = new List<string>();
            //listUrl.Add("pol_document_urls");
            //-------------------------------------------------------------//

            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            string logTest = "";

            #region JsonSerializerSettings
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.Formatting = Formatting.Indented;
            #endregion

            #region Prepare Data

            //debl.debuglog("claim_insrd_ids :" + jsonStringFormat["claim_insrd_ids"]);
            //debl.debuglog("claim_icd_10_codes :" + jsonStringFormat["claim_icd_10_codes"]);

            jsonStringFormat["claim_insrd_ids"] = JValue.CreateNull();
            jsonStringFormat["claim_icd_10_codes"] = JValue.CreateNull();
            jsonStringFormat["claim_icd_9_codes"] = JValue.CreateNull();


            //debl.debuglog("claim_insrd_ids :" + jsonStringFormat["claim_insrd_ids"]);
            //debl.debuglog("claim_icd_10_codes :" + jsonStringFormat["claim_icd_10_codes"]);

            //Console.WriteLine("In Json: " + jsonStringFormat);
            string pocStr = JsonConvert.SerializeObject(jsonStringFormat);

            //debl.debuglog("pocStr :" + pocStr);
          



            //Console.WriteLine("-------------------------------------");
            //Console.WriteLine("posStr : " + pocStr);

            ClaimModel poc = JsonConvert.DeserializeObject<ClaimModel>(pocStr);

            debl.debuglog("poc :" + poc);

            ClaimPaymentPrg claimPaymentPrg = new ClaimPaymentPrg();

            //Console.WriteLine("PRE DATA => ");
            //Console.WriteLine(poc);
            //Console.WriteLine(JsonConvert.SerializeObject(poc, serializerSettings));
            // Console.WriteLine("-------------------------------------\n");
            #endregion


            //------- Encrypt data ---------
            //------- Encrypt data ---------
            string inputValue;
            string encryptText = "";
            Object jsonarry = "";
            //string value = "";
            //var attr = "";

            try
            {
                foreach (var prop in poc.GetType().GetProperties())
                {
                    var attr = poc.GetType().GetProperty(prop.Name);
                    var value = attr.GetValue(poc, null);

                    //debl.debuglog("Prop:" + prop.Name + " - "+ value);

                    //Console.WriteLine("Prop.Name : " + prop.Name + " - " + value + "\n");

                    /**************************** Switch check More JsonObject *******************************/
                   try
                    {
                        switch (prop.Name)
                        {
                            case "claim_coverages":
                                //Console.WriteLine("Customer value: " + value.ToString());
                                //Console.WriteLine("Check Type : Json Object \n");
                                if (value != null)
                                {
                                    String ResultJObject = claimcoveragesfunc(sensitiveFieldList, value, pubKeyCentralPath);
                                        try
                                        {
                                            dynamic parsedJson = JsonConvert.DeserializeObject(ResultJObject);
                                            string encrypeOut = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                                            //Console.WriteLine("Main result: "+ encrypeOut + "\n");
                                            JArray json = JArray.Parse(encrypeOut);
                                            //Console.WriteLine("Aray result: "+ json + "\n");
                                            attr.SetValue(poc, json);
                                            debl.debuglog("Endd claim coverage");
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

                        case "estimates":
                            //Console.WriteLine("Customer value: " + value.ToString());
                            //Console.WriteLine("Check Type : Json Object \n");
                            if (value != null)
                            {
                                String ResultJObject = estimatesfunc(sensitiveFieldList, value, pubKeyCentralPath);
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

                        case "claim_payments":
                            //Console.WriteLine("Customer value: " + value.ToString());
                            //Console.WriteLine("Check Type : Json Object \n");
                            if (value != null)
                            {
                                string ResultJObject = claimpaymentfunc(sensitiveFieldList, value, pubKeyCentralPath);
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
                                    //Console.WriteLine("Error attr.SetValue: " + ex);
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
                                
                                if (value == null || value == "")
                                {
                                    attr.SetValue(poc, null);
                                }
                                else
                                {
                                    if (new[] { "claim_incurred_date", "claim_report_date", "claim_notify_date", "claim_discharge_date", "claim_completion_date", "claim_close_date" }.Contains(prop.Name) )
                                    {
                                        value = dateTimeConvert(value.ToString().Trim());

                                    }

                                    bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                                    //Console.WriteLine("claim: " + prop.Name + ": " + value);
                                    //debl.debuglog("SensitiveFields name:" + prop.Name + " = " +alreadyExist.ToString() );

                                    if (alreadyExist)
                                    {
                                        //debl.debuglog("Public Key : " + pubKeyCentralPath);
                                        inputValue = value.ToString().Trim();
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
                     
                  }
                catch(Exception ex)
                {
                    //Console.WriteLine("Error json policy !!!");
                    debl.debuglog("Error 1: "+ ex.Message);
                }
                  
                    
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex + " Encrypt => Can't find public-key ibsl. \n");
                debl.debuglog("Error 2: " + ex.Message);
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
            EditClaimModel model = new EditClaimModel();
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
                //Console.WriteLine("IsValid : " + isValid);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Verify => Can't find public-key company. \n");
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
            string text = System.IO.File.ReadAllText(centralKeyPath);
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
                throw (ex);
            }
        }
        private static string Signing(byte[] rawData, string compKeyPath)
        {
            string text = System.IO.File.ReadAllText(compKeyPath);
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
