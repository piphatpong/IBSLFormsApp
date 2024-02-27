using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using static IBSLFormsApp.Model.Policy.Policy_fund_code_Program;
using IBSLFormsApp.Model.Libraries;
using IBSLFormsApp.Model.MapDataPolicy;

namespace IBSLFormsApp.Model.Policy
{
    internal class InvestmentsProgram
    {
        public static string investmentsProgram(List<string> sensitiveFieldList, List<string> listUrl, object jsonContent, string pubKeyCentralPath)
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

                Investments pocInsureds = JsonConvert.DeserializeObject<Investments>(insuresdJson);

                //Console.WriteLine("item2: " + pocInsureds);

                try
                {
                    foreach (var prop in pocInsureds.GetType().GetProperties())
                    {
                        var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);
                        var value = attrInsurdes.GetValue(pocInsureds, null);

                        //Console.WriteLine("Investment Test : " + prop.Name.Trim() + "-" + value + "\n");

                        bool results = listUrl.Contains(prop.Name);

                        if (value == null || value is "[]")
                        {
                            encryptText = value == null ? null : "[]";
                        }
                        else
                        {
                            /**************** Check Sensitive Field List *****************/
                            bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                            //Console.WriteLine("Sensitive: " +  alreadyExist);
                            //Console.WriteLine("-----------------------Investment Test-------------------------");
                            bool checkJson = IsValidJson.IsValidJsonFormat(value.ToString());
                            if (alreadyExist || checkJson)
                            {
                                /**************************** Switch check More JsonObject *******************************/
                                switch (prop.Name.Trim())
                                {

                                    case "policy_fund_codes":
                                        //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
                                        //Console.WriteLine("Before InsuredsProg value: " + value.ToString());

                                        string resultJArray = policy_fund_code_Program(sensitiveFieldList, listUrl, value, pubKeyCentralPath);
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
                                        /**************** Check Customer Date time and Convert ****************/
                                        if (prop.Name == "inv_transaction_date")
                                        {
                                            value = DateTimeConvert.dateTimeConvert(value.ToString());

                                        }
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
