using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using static IBSLFormsApp.Model.Policy.Cov_plan_cov_Program;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using IBSLFormsApp.Model.Libraries;
using IBSLFormsApp.Model.MapDataPolicy;

namespace IBSLFormsApp.Model.Policy
{
    internal class Pol_cov_plan_seq_Program
    {
        public static string pol_cov_plan_seq_Program(List<string> sensitiveFieldList, List<string> listUrl, object jsonContent, string pubKeyCentralPath)
        {
            //string privKeyCentralPath = "ibsl.key";
            //string pubKeyCentralPath = "ibsl.crt";
            //string privKeycompPath = "company.key";
            //string pubKeycompPath = "company.crt";
            string inputValue;
            string encryptText = "";
            string preItem = "";
            string insuresdJson = JsonConvert.SerializeObject(jsonContent);
            JsonArray jsonArrayEncrp = new JsonArray();

            //Console.WriteLine("Insure PolicyProgram jsonContent : " + insuresdJson);
            dynamic jsonarry = JsonConvert.DeserializeObject(insuresdJson);
            //Console.WriteLine("Insure PolicyProgram: " +  jsonarry.ToString());

            foreach (var item in jsonarry)
            {
                //Console.WriteLine("item1: " + item.ToString());

                insuresdJson = JsonConvert.SerializeObject(item);

                Policy_Coverage_Plan_Sequence pocInsureds = JsonConvert.DeserializeObject<Policy_Coverage_Plan_Sequence>(insuresdJson);

                //Console.WriteLine("item2: " + pocInsureds);

                try
                {
                    foreach (var prop in pocInsureds.GetType().GetProperties())
                    {
                        var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);
                        var value = attrInsurdes.GetValue(pocInsureds, null);

                        //Console.WriteLine("Pol cov plan seq Test : " + prop.Name.Trim() + "-" + value + "\n");

                        bool results = listUrl.Contains(prop.Name);

                        if (value == null || value is "[]")
                        {
                            encryptText = value == null ? "" : "[]";
                            attrInsurdes.SetValue(pocInsureds, encryptText);

                        }
                        else
                        {
                            /**************** Check Sensitive Field List *****************/
                            bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                            //Console.WriteLine("Sensitive: " + alreadyExist);
                            //Console.WriteLine("-----------------------Pol cov plan seq Test-------------------------");
                            bool checkJson = IsValidJson.IsValidJsonFormat(value.ToString());
                            if (alreadyExist || checkJson)
                            {
                                /**************************** Switch check More JsonObject *******************************/
                                switch (prop.Name.Trim())
                                {
                                    case "coverages_under_plans":
                                        //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
                                        //Console.WriteLine("Before coverages_under_plans value: " + value.ToString());

                                        string resultJArray = cov_plan_cov_Program(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
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

                                    default:
                                        //Console.WriteLine("After value: " + value.ToString());
                                        //Console.WriteLine("Check Type : NOT Json Object policy \n");
                                        inputValue = value.ToString();
                                        encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                        attrInsurdes.SetValue(pocInsureds, encryptText);
                                        break;
                                }

                            }
                            else
                            {
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex + " Encrypt => Can't find public-key ibsl. \n");
                }
            }

            //jsonArrayEncrp.Add(preItem.Substring(1));
            preItem = "[" + preItem + "]";

            //Console.WriteLine("jsonArrayEncrp.ToJsonString(): " + preItem);

            return preItem;
        }
    }
}
