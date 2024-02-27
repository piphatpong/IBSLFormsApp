using Newtonsoft.Json;
using static IBSLFormsApp.Model.Libraries.IsValidJson;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using IBSLFormsApp.Model.MapDataPolicy;
using IBSLFormsApp.Model.Libraries;
using System.ComponentModel;

namespace IBSLFormsApp.Model.Policy
{
    internal class InsuredsProgram
    {
        public static string insuredsProgram(List<string> sensitiveFieldList, object jsonContent, string pubKeyCentralPath)
        {
            //********************** Set colume URL [] ********************//
            List<string> listUrl = new List<string>();
            /*************** Log Test ***************/
            DebugLog deblog = new DebugLog();

            string inputValue;
            string encryptText = "";
            string preItem = "";
            string insuresdJson = JsonConvert.SerializeObject(jsonContent);

            dynamic jsonarry = JsonConvert.DeserializeObject(insuresdJson);

            //deblog.debuglog("log array: " + jsonarry.ToString());
            try
            {
                foreach (var item in jsonarry)
                {
                        //deblog.debuglog("item insured : " + item.ToString());

                        string insuresdConvert = JsonConvert.SerializeObject(item);

                    //deblog.debuglog("insCon:" + insuresdConvert);

                    InsuredsMaps pocInsureds = JsonConvert.DeserializeObject<InsuredsMaps>(insuresdConvert);

                    /*************** Log Test ***************/
                    //string logTest = "pocInsureds : ";
                    //deblog.debuglog(logTest + pocInsureds);

               
                        foreach (var prop in pocInsureds.GetType().GetProperties())
                        {
                            var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);
                            var value = attrInsurdes.GetValue(pocInsureds, null);

                            //Console.WriteLine("Insureds Test : " + prop.Name.Trim() + "-" + value + "\n");

                            /*************** Log Test ***************/
                            //logTest = "get name insure 1: " + prop.Name + " = " + value;
                            //deblog.debuglog(logTest);

                            if (value == null || value == "[]")
                            {
                                encryptText = value == null ? null : "[]";
                                attrInsurdes.SetValue(pocInsureds, encryptText);
                                /*************** Log Test ***************/
                                //logTest = "get name 2: " + prop.Name + " = " + value;
                                //deblog.debuglog(logTest);
                            }
                            else
                            {
                                /**************** Check Sensitive Field List *****************/
                                bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                            
                                bool checkJson = IsValidJsonFormat(value.ToString());
                                if (alreadyExist || checkJson)
                                {
                                    /**************************** Switch check More JsonObject *******************************/
                                    switch (prop.Name.Trim())
                                    {
                                        case "insrd_beneficiaries":
                                            /*************** Log Test ***************/
                                            //logTest = "insrd_beneficiaries : " + prop.Name + " = " + value;
                                            //deblog.debuglog(logTest);

                                            string resultJArray = insuredBeneficiary_Program.insuredbeneficiary(sensitiveFieldList, value, pubKeyCentralPath);
                                            try
                                            {
                                                JArray result = JArray.Parse(resultJArray);
                                                attrInsurdes.SetValue(pocInsureds, result);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error attr.SetValue" + ex);
                                            }
                                            break;

                                        case "insrd_pol_sub_ids":
                                            //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
                                            //Console.WriteLine("Before InsuredsProg value: " + value.ToString());
                                            string resultJA = Insrd_Pol_Sub_Ids_Prg.insrd_pol_sub_ids_prg(sensitiveFieldList , value, pubKeyCentralPath);
                                            try
                                            {
                                                JArray result = JArray.Parse(resultJA);
                                                attrInsurdes.SetValue(pocInsureds, result);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error attr.SetValue" + ex);
                                            }
                                            break;

                                    

                                        default:
                                       
                                            inputValue = value.ToString();
                                            encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                            attrInsurdes.SetValue(pocInsureds, encryptText);
                                            break;
                                    }
                                }
                                else
                                {
                                    if (prop.Name == "insrd_birthday")
                                    {
                                        value = DateTimeConvert.dateTimeConvert(value.ToString());
                                    }
                                    attrInsurdes.SetValue(pocInsureds, value);
                                }
                            }
                            preItem = JsonConvert.SerializeObject(pocInsureds);
                            //jsonArrayEncrp.Add(preItem.ToString());
                            //Console.WriteLine("***************************** Insureds Test ********************************");
                            //Console.WriteLine("preItem: " + preItem.Substring(1));

                            //ConsoleKeyInfo restart = Console.ReadKey();

                        }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "Encrypt => Can't find public-key ibsl. \n");
                deblog.debuglog(ex.ToString());
            }

            //jsonArrayEncrp.Add(preItem.Substring(1));
            preItem = "[" + preItem + "]";

            //Console.WriteLine("jsonArrayEncrp.ToJsonString(): " + preItem);

            return preItem;
        }


    }
}
