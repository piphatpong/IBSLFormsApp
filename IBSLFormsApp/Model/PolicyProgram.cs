using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Text;
using static IBSLFormsApp.Model.Policy.CustomerProgram;
using static IBSLFormsApp.Model.Policy.InsuredsProgram;
using static IBSLFormsApp.Model.Policy.Sub_policy_Program;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using static IBSLFormsApp.Model.Libraries.DateTimeConvert;
using static IBSLFormsApp.Model.Policy.BeneficiariesProgram;
using static IBSLFormsApp.Model.Policy.EndorsementsProgram;
using static IBSLFormsApp.Model.Policy.LoansProgram;
using static IBSLFormsApp.Model.Policy.Loan_payment_Program;
using static IBSLFormsApp.Model.Policy.InvestmentsProgram;
using static IBSLFormsApp.Model.Policy.CheckInsBen;
using IBSLFormsApp.Model.Policy;
using IBSLFormsApp.Model.Libraries;
using IBSLFormsApp.Helpers;
using IBSLFormsApp.Model.MapDataPolicy;

namespace IBSLFormsApp
{
    public class PolicyProgram
    {
        string startFolderPath = Application.StartupPath;

        string pathfilename = "\\log\\";

        string key_channel = "";

        /*************** Log Test ***************/
        //string logTest = "Count sql: " + reader.FieldCount + " store proc:" + storeProc;
        DebugLog debl = new DebugLog();
        string logTest = "**********";

        public async Task <String> policyprogram(string storeProc, string servername, string usrName, string pwd)
        {
            dynamic restxt = null;

            string urlPostRequest = "";

            MapPocLog logModel = new MapPocLog();
            
            var reqTokenAPI = new ReqestTokenApi();

            var timestamp = DateTime.Now.ToFileTime();

            dynamic jsonArray = null;
            dynamic itemArray = null;

            //*** Create Folder
            if (!Directory.Exists(startFolderPath + pathfilename))
            {
                Directory.CreateDirectory(startFolderPath + pathfilename);
            }

            string ResponsePostForToken = await reqTokenAPI.requesttokenapi(servername, usrName, pwd);

            JObject TokenJson = JObject.Parse(ResponsePostForToken);
            string access_token = TokenJson["token_type"].ToString()+ " "+ TokenJson["access_token"].ToString();
            //Console.WriteLine(TokenJson.ToString());

            //logTest = "TokenJson.ToString()" + TokenJson.ToString();
            //debl.debuglog("============"+storeProc+"///////// \n"+logTest);

            if (servername == "uat")
            {
                urlPostRequest = "https://ibsl-uat-api.oic.or.th";
                key_channel = "ibsl_UAT.crt";
                pathfilename = "\\log\\UAT_ibsl_pol_";
            }
            else
            {
                urlPostRequest = "https://ibsl-api.oic.or.th";
                key_channel = "ibsl_PRD.crt";
                pathfilename = "\\log\\PRD_ibsl_pol_";
            }
            string exportfile = startFolderPath + pathfilename  + timestamp + ".txt";

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

            IbslUatPolicyApi ibsluatapi = new IbslUatPolicyApi();

            //Console.WriteLine("Test: " + jsonarry.ToString());

            SensitiveFields sensitive = new SensitiveFields();
            var sensFil = sensitive.SensFields();
            List<string> sensitiveFieldList = new List<string>();
            foreach (var vl in sensFil)
            {
                sensitiveFieldList.Add(vl.ToString());
            }

            try
            {
                //String connectionString = "Data Source=10.20.25.101;Initial Catalog=IBS_Life;User ID=devconnect;Password=P@ssw0rd1234";
                String connectionString = "Data Source=VELA\\SQLEXPRESS;Initial Catalog=IBS_Life;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storeProc, connection))
                    {
                        connection.Open();
                        var jsonResult = new StringBuilder();

                        try
                        {
                            SqlDataReader reader = cmd.ExecuteReader();

                            //var xxx = reader.Read();
                            // debl.debuglog(" Reader : " + xxx.ToString());

                            while (reader.Read())
                            {
                                jsonResult.Append(reader.GetValue(0).ToString());
                                /*----------------------------------------------*/
                                //debl.debuglog("JsonResult : "+ jsonResult.ToString());
                            }

                            var Result_json = "";
                            var ResultSet = new List<string>();

                            jsonArray = JsonConvert.DeserializeObject(jsonResult.ToString());

                            //foreach (var item in jsonArray)
                            //{
                            //    itemArray = JsonConvert.DeserializeObject(item.ToString());
                            //    debl.debuglog("ITEM insureds: " + itemArray["insrd_beneficiaries"]);
                            //}

                            /*************** Log Test ***************/
                            logTest = "from JsonArray=>" + jsonArray;

                            //debl.debuglog(logTest);

                            //debl.debuglog("insur_ben:" + jsonArray["insureds"].ToString());
                        } 
                        catch (Exception ex)
                        {
                            debl.debuglog("Err Exception read json sql: " + ex.Message);
                        }

                        connection.Close();

                    }// ----- sql command ----- //
                }

                string gotOutput = "";
                string goutjson = "";
                string ref_seq = "";
                string pol_id = "";
                string tranc_res = "";

                string send_res_code = "";
                string send_res_msg = "";
                string send_tranc_status = "";


                // if (true) { 
                JObject dataJObject = new JObject();

                foreach (var item in jsonArray)
                {
                    /**** for update status in policy table ****/
                    ref_seq = item["ref_seq"];
                    pol_id = item["pol_id"];

                    JObject check_ins_ben = checkInsBen(item);
                    //debl.debuglog(" check_ins_ben: " + check_ins_ben);

                    //************* Check Insurance Beneficiary **********//
                    //if (check_ins_ben == "cross") { continue; } ;

                    /*************** Log Test ***************/
                    //debl.debuglog("item1: " + item["insureds"][0]);
                   
                    string playloadResult = EncyProc(sensitiveFieldList, check_ins_ben, key_channel);

                    //debl.debuglog("response: " + playloadResult);

                    dynamic parsedJson = JsonConvert.DeserializeObject(playloadResult);

                    /*************** Log Test ***************/
                    //logTest = "After Ency: " + parsedJson.ToString();
                    //debl.debuglog(logTest);

                    //--- for export to log file ---//

                    ///////-string response = await ibsluatapi.ibsluatapi(access_token, urlPostRequest, parsedJson);

                    var response = Task.Run(async () => await ibsluatapi.ibsluatpolicyapi(access_token, urlPostRequest, parsedJson)).Result;

                    restxt = JsonConvert.DeserializeObject(response);

                    #region Export to Log
                    logModel.Body = parsedJson;
                    logModel.Response = restxt;

                    //**************************  for indent format in log file ******************//
                    string logResTxt2 = JsonConvert.SerializeObject(logModel, Formatting.Indented);

                    //**************************  one record one line in log file ******************//
                    //string logResTxt2 = JsonConvert.SerializeObject(logModel);

                    #endregion

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


                    //debl.debuglog("ref_seq: " + ref_seq + " > response: " + tranc_status + " / " + tranc_res);

                    //********************** UPDATE API REQUEST STATUS **********************//

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string upresp_status = @" UPDATE Temp_policies_26_2_67 SET pol_transaction_status = '" + record_status + "' ,send_status = '" + send_tranc_status + "', send_res = '" + tranc_res + "' " +
                            " ,send_res_code = '" + send_res_code + "', send_res_msg='" + send_res_msg + "'" +
                            " WHERE ref_seq = '" + ref_seq + "' and pol_id = '" + pol_id + "'";

                        using (SqlCommand updateStatusCommand = new SqlCommand(upresp_status, conn))
                        {
                            conn.Open();
                            //await debl.debuglog("xxxxxxxx");
                            updateStatusCommand.ExecuteNonQuery();
                            conn.Close();
                        };
                        //debl.debuglog("query: " + upresp_status);
                    } 
                } // end for //
               // }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Exception: " + ex.ToString());
                debl.debuglog("Error Exception 1: " + ex.Message);

            }
            
            return restxt.ToString();

        }

        private static string EncyProc(List<string> sensitiveFieldList, JObject jsonStringFormat, string key_channel)
        {
            string startFolderPath = Application.StartupPath;

            //Console.Clear();
            string getEncryp = "";
            string privKeyCentralPath = startFolderPath + "\\SSLKey\\ibsl.key";

            string pubKeyCentralPath = startFolderPath + "\\SSLKey\\" + key_channel;
          
            string privKeycompPath = startFolderPath + "\\SSLKey\\company.key";
            
            string pubKeycompPath = startFolderPath + "\\SSLKey\\company.crt";
            

            //********************** Set colume URL [] ********************//
            List<string> listUrl = new List<string>();
            listUrl.Add("pol_document_urls");
            //-------------------------------------------------------------//
            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            string logTest = "";
            //string logTest = "In Encyp: " + jsonStringFormat.ToString();
            //debl.debuglog(logTest);

            #region JsonSerializerSettings
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.Formatting = Formatting.Indented;
            #endregion

            #region Prepare Data

            //Console.WriteLine("In Json: " + jsonStringFormat);

            string pocStr = JsonConvert.SerializeObject(jsonStringFormat);
            dynamic parsedJson = JsonConvert.DeserializeObject(pocStr);
            //string logTest = "jsonStringFormat : " + jsonStringFormat;
            //debl.debuglog(logTest);

            //Console.WriteLine("-------------------------------------");
            //Console.WriteLine("posStr : " + pocStr);
            string logResTxt2 = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            //logTest = "payload tt: " + logResTxt2;
            //debl.debuglog(logTest);

            PocModel poc = JsonConvert.DeserializeObject<PocModel>(jsonStringFormat.ToString());  // -----ผิดตรงนี้แหละ 

            // Console.WriteLine("-------------------------------------\n");
            #endregion
            //------- Encrypt data ---------
            //------- Encrypt data ---------
            string inputValue;
            string encryptText = "";
            String resultJArray = "";
            string preItemxx = "";
            Object jsonarry = "";
            bool checkBenefi = true;
            //string value = "";
            //var attr = "";

            try
            {
                foreach (var prop in poc.GetType().GetProperties())
                {
                    var attr = poc.GetType().GetProperty(prop.Name);
                    var value = attr.GetValue(poc, null);

                    //logTest = "pol foreach : " + attr;
                    //debl.debuglog("\nforeach: prop name ="+prop.Name+"/ "+value);

                    /**************************** Switch check More JsonObject *******************************/
                    /**try
                    {**/
                    switch (prop.Name)
                        {
                            case "customer":
                                //Console.WriteLine("Customer value: " + value.ToString());
                                //Console.WriteLine("Check Type : Json Object \n");
                                //logTest = "customer name : " + prop.Name.Trim() + "= " + value.ToString();
                                //debl.debuglog(logTest);

                                if (value != null)
                                    {
                                        String ResultJObject = customerProgram(sensitiveFieldList, value, pubKeyCentralPath);
                                        try
                                        {
                                            JObject json = JObject.Parse(ResultJObject);
                                            attr.SetValue(poc, json);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Error attr.SetValue" + ex);
                                        }
                                }
                                else
                                    {
                                        Array valay = new Array[] { };
                                        var DumArray = JArray.FromObject(valay);
                                        Console.WriteLine("valay: " + DumArray);
                                        attr.SetValue(poc, DumArray);
                                        //attr.SetValue(poc, value);
                                    }
                                break;

                            case "insureds":
                                if (value != null)
                                {
                                    //Console.WriteLine("insureds value: " + value.ToString());
                                    //Console.WriteLine("Check Type : Json Object \n");

                                    resultJArray = insuredsProgram(sensitiveFieldList, value, pubKeyCentralPath);
                                    try
                                    {
                                        JArray result = JArray.Parse(resultJArray);
                                        attr.SetValue(poc, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error attr.SetValue" + ex);
                                    }
                                    preItemxx = JsonConvert.SerializeObject(poc);
                                    jsonarry = JsonConvert.DeserializeObject(preItemxx);

                                    //Console.WriteLine("Test: " + jsonarry.ToString());
                                    //ConsoleKeyInfo restart = Console.ReadKey();
                                }
                                else
                                {
                                    Array valay = new Array[] {};
                                    var DumArray = JArray.FromObject(valay);
                                    //Console.WriteLine("valay: "+ DumArray);
                                    attr.SetValue(poc, DumArray);
                                    //attr.SetValue(poc, value);
                                }
                                break;

                            case "sub_policies":
                                //Console.WriteLine("sub_policy value: " + value.ToString());
                                //Console.WriteLine("+++++++++++++ sub policy +++++++++++++++");
                                //debl.debuglog("sub_policies: " + prop.Name + " = " + value);
                                if (value != null)
                                {
                                    resultJArray = sub_pol_Program(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
                                    try
                                    {
                                        JArray result = JArray.Parse(resultJArray);
                                        attr.SetValue(poc, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error attr.SetValue" + ex);
                                    }
                                    preItemxx = JsonConvert.SerializeObject(poc);
                                    jsonarry = JsonConvert.DeserializeObject(preItemxx);

                                    //Console.WriteLine("Test: " + jsonarry.ToString());
                                    //ConsoleKeyInfo restart = Console.ReadKey();
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

                            case "beneficiaries":
                            //Console.WriteLine("coverage value: " + value.ToString());
                            //Console.WriteLine("Check Type : Json Object \n");
                            //debl.debuglog("benefi_main:" + prop.Name + " Value: " + value);
                                if (value != null)
                                {
                                    resultJArray = beneficiariesProgram(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
                                    try
                                    {
                                        JArray result = JArray.Parse(resultJArray);
                                        attr.SetValue(poc, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error attr.SetValue" + ex);
                                    }
                                    preItemxx = JsonConvert.SerializeObject(poc);
                                    jsonarry = JsonConvert.DeserializeObject(preItemxx);

                                    //Console.WriteLine("Test Beneficiary: " + jsonarry.ToString());
                                    //ConsoleKeyInfo restart = Console.ReadKey();
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

                            case "endorsements":
                                //Console.WriteLine("endorsements value: " + value.ToString());
                                //Console.WriteLine("Check Type : Json Object \n");

                                if (value != null)
                                {
                                    resultJArray = endorsementsProgram(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
                                    try
                                    {
                                        JArray result = JArray.Parse(resultJArray);
                                        attr.SetValue(poc, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error attr.SetValue" + ex);
                                    }
                                    preItemxx = JsonConvert.SerializeObject(poc);
                                    jsonarry = JsonConvert.DeserializeObject(preItemxx);

                                    //Console.WriteLine("Test: " + jsonarry.ToString());
                                    //ConsoleKeyInfo restart = Console.ReadKey();
                                    
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

                            case "loans":
                                //Console.WriteLine("endorsements value: " + value.ToString());
                                //Console.WriteLine("Check Type : Json Object \n");

                                if (value != null)
                                {
                                    resultJArray = loansProgram(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
                                    try
                                    {
                                        JArray result = JArray.Parse(resultJArray);
                                        attr.SetValue(poc, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error attr.SetValue" + ex);
                                    }
                                    preItemxx = JsonConvert.SerializeObject(poc);
                                    jsonarry = JsonConvert.DeserializeObject(preItemxx);

                                    //Console.WriteLine("Test: " + jsonarry.ToString());
                                    //ConsoleKeyInfo restart = Console.ReadKey();
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

                            case "loan_payments":
                                //Console.WriteLine("loan payment value: " + value.ToString());
                                if (value != null)
                                {
                                    resultJArray = loan_payment_Program(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
                                    try
                                    {
                                        JArray result = JArray.Parse(resultJArray);
                                        attr.SetValue(poc, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error attr.SetValue" + ex);
                                    }
                                    preItemxx = JsonConvert.SerializeObject(poc);
                                    jsonarry = JsonConvert.DeserializeObject(preItemxx);

                                    //Console.WriteLine("Test: " + jsonarry.ToString());
                                    //ConsoleKeyInfo restart = Console.ReadKey();
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

                            case "investments":
                                //Console.WriteLine("investments value: " + value.ToString());
                                if (value != null)
                                {
                                    resultJArray = investmentsProgram(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
                                    try
                                    {
                                        JArray result = JArray.Parse(resultJArray);
                                        attr.SetValue(poc, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error attr.SetValue" + ex);
                                    }
                                    preItemxx = JsonConvert.SerializeObject(poc);
                                    jsonarry = JsonConvert.DeserializeObject(preItemxx);

                                    //Console.WriteLine("Test: " + jsonarry.ToString());
                                    //ConsoleKeyInfo restart = Console.ReadKey();
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
                                if (prop.Name == "pol_product_type_other_detail")
                                    {
                                        //debl.debuglog("pol_detail");

                                        if (value is not null)
                                        {
                                            if (value.ToString().Length > 50)
                                            {
                                                value = value.ToString().Substring(0, 49);
                                                //debl.debuglog("pol_product_type_other_detail inloop \n");
                                            }
                                        }
                                        else
                                        {
                                            value = "";
                                            //debl.debuglog("pol_product_type_other_detail else \n");
                                        }
                                    }
                                if (prop.Name =="pol_approved_date" && value != null)
                                    {
                                        value = dateTimeConvert(value);
                                        //debl.debuglog("pol_approved_date" + value.ToString());
                                    }
                                if (prop.Name == "pol_start_date" && value is not null)
                                    {
                                        value = dateTimeConvert(value);
                                        //debl.debuglog("pol_start_date" + value.ToString());
                                    }
                                if (prop.Name == "pol_end_date" && value != null)
                                    {
                                        value = dateTimeConvert(value);
                                        //debl.debuglog("pol_end_date" + value.ToString());
                                    }
                                //---- Check pol_document_urls --------//
                                //bool checkUrl = listUrl.Contains(prop.Name);

                                bool checkUrl = false;

                                if (checkUrl)
                                {
                                    //value = "[" + value + "]";
                                    Array valay = new String[] { };
                                    attr.SetValue(poc, valay);
                                    
                                    //Console.WriteLine("Compare : " + valay.ToString() + "\n");

                                }
                                else if (value == null)
                                {
                                    attr.SetValue(poc, value);
                                }
                                else
                                {
                                    bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                                    //Console.WriteLine("Policy: " + prop.Name + ": " + value);
                                    //debl.debuglog("alreadyExist: " + alreadyExist.ToString());
                                    if (alreadyExist)
                                    {
                                        inputValue = value.ToString();
                                        encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                        attr.SetValue(poc, encryptText);
                                        //debl.debuglog("Policy: " + prop.Name + ": " + encryptText);
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
            EditPocModel model = new EditPocModel();
            model.payload = poc;
            model.Signature = signature;
            //Console.WriteLine("PAYLOAD => ");
            //Console.WriteLine("----------------------------------");
            //Console.WriteLine(JsonConvert.SerializeObject(model, serializerSettings));
            //Console.WriteLine("----------------------------------\n");
            #endregion

            getEncryp = JsonConvert.SerializeObject(model);

            //------- Verify data ---------
            //Console.WriteLine("------- VERIFY -------");
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
                Console.WriteLine("Verify => Can't find public-key company. \n");
            }

            //------- Decrypt data ---------
            if (isValid)
            {
                try
                {
                    string decryptText;

                    //Console.WriteLine("\n------- DECRYPT -----------");
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
                                //inputValue = value.ToString();
                                //*** decryptText = DecryptData(value.ToString(), privKeyCentralPath);
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
