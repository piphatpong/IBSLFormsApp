using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using static IBSLFormsApp.Model.Policy.Pol_cov_plan_seq_Program;
using static IBSLFormsApp.Model.Policy.CoverageProgram;
using static IBSLFormsApp.Model.Libraries.DateTimeConvert;
using IBSLFormsApp.Model.Libraries;
using IBSLFormsApp.Model.MapDataPolicy;

namespace IBSLFormsApp.Model.Policy
{
    internal class Sub_policy_Program
    {
        public static string sub_pol_Program(List<string> sensitiveFieldList, List<string> listUrl, object jsonContent, string pubKeyCentralPath)
        {
            //string privKeyCentralPath = "ibsl.key";
            //string pubKeyCentralPath = "ibsl.crt";
            //string privKeycompPath = "company.key";
            //string pubKeycompPath = "company.crt";
            string inputValue;
            string encryptText = "";
            string preItem = "";
            string resultJArray = "";
            string insuresdJson = JsonConvert.SerializeObject(jsonContent);
            JsonArray jsonArrayEncrp = new JsonArray();

            //Console.WriteLine("subPolicy PolicyProgram jsonContent : " + insuresdJson);
            dynamic jsonarry = JsonConvert.DeserializeObject(insuresdJson);
            //Console.WriteLine("Sub policy: " +  jsonarry.ToString());

            foreach (var item in jsonarry)
            {
                //Console.WriteLine("item1: " + item.ToString());

                insuresdJson = JsonConvert.SerializeObject(item);

                Insrd_Po_Sub_Policy pocInsureds = JsonConvert.DeserializeObject<Insrd_Po_Sub_Policy>(insuresdJson);

                //Console.WriteLine("item2: " + pocInsureds);

                /* try
                 { */
                foreach (var prop in pocInsureds.GetType().GetProperties())
                {
                    var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);
                    var value = attrInsurdes.GetValue(pocInsureds, null);

                    //Console.WriteLine("Sub pol Test : " + prop.Name.Trim() + "-" + value + "\n");

                    if (value == null || value is "[]")
                    {
                        encryptText = value == null ? null : "[]";
                        attrInsurdes.SetValue(pocInsureds, encryptText);
                    }
                    else
                    {
                        /**************** Check Sensitive Field List *****************/
                        bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                        //Console.WriteLine("Sensitive: " + alreadyExist);
                        //Console.WriteLine("-----------------------Sub pol Test-------------------------");
                        bool checkJson = IsValidJson.IsValidJsonFormat(value.ToString());
                        if (alreadyExist || checkJson)
                        {
                            /**************************** Switch check More JsonObject *******************************/
                            switch (prop.Name.Trim())
                            {
                                case "policy_cov_plan_seqs":
                                    //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
                                    //Console.WriteLine("Before policy_cov_plan_seqs value: " + value.ToString());
                                    if (value == null) {
                                        attrInsurdes.SetValue(pocInsureds, null);
                                    }
                                    else
                                    {
                                        resultJArray = pol_cov_plan_seq_Program(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
                                        try
                                        {
                                            JArray result = JArray.Parse(resultJArray);
                                            attrInsurdes.SetValue(pocInsureds, result);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Error attr.SetValue" + ex);
                                        }
                                    }
                                    break;


                                case "coverages":
                                    //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
                                    //Console.WriteLine("Before coverages value: " + value.ToString());

                                    resultJArray = coverageProgram(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
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
                            switch (prop.Name.Trim())
                            {
                                case "sub_pol_start_date":
                                    value = dateTimeConvert(value.ToString());
                                    //inputValue = value.ToString();
                                    //encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                    attrInsurdes.SetValue(pocInsureds, value);
                                    break;

                                case "sub_pol_end_date":
                                    value = dateTimeConvert(value.ToString());
                                    //inputValue = value.ToString();
                                    //encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                    attrInsurdes.SetValue(pocInsureds, value);
                                    break;

                                default:
                                    attrInsurdes.SetValue(pocInsureds, value);
                                    break;
                            }
                        }
                    }

                    preItem = JsonConvert.SerializeObject(pocInsureds);

                }
                /** }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex + " Encrypt => Can't find public-key ibsl. \n");
                 } */
            }

            //jsonArrayEncrp.Add(preItem.Substring(1));
            preItem = "[" + preItem + "]";

            //Console.WriteLine("jsonArrayEncrp.ToJsonString(): " + preItem);

            return preItem;
        }
    }
}
