using Newtonsoft.Json;
using IBSLFormsApp.Model.Libraries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using IBSLFormsApp.Model.MapDataPolicy;

namespace IBSLFormsApp.Model.Policy
{
    internal class Loan_payment_Program
    {
        public static string loan_payment_Program(List<string> sensitiveFieldList, List<string> listUrl, object jsonContent, string pubKeyCentralPath)
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

            //Console.WriteLine("Loan PolicyProgram jsonContent : " + insuresdJson);
            dynamic jsonarry = JsonConvert.DeserializeObject(insuresdJson);
            //Console.WriteLine("Loan PolicyProgram: " +  jsonarry.ToString());

            foreach (var item in jsonarry)
            {
                //Console.WriteLine("item1: " + item.ToString());

                insuresdJson = JsonConvert.SerializeObject(item);

                Loan_payment pocInsureds = JsonConvert.DeserializeObject<Loan_payment>(insuresdJson);

                //Console.WriteLine("item2: " + pocInsureds);

                try
                {
                    foreach (var prop in pocInsureds.GetType().GetProperties())
                    {
                        var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);
                        var value = attrInsurdes.GetValue(pocInsureds, null);

                        //Console.WriteLine("Loan payment Test : " + prop.Name.Trim() + "-" + value + "\n");

                        //bool results = listUrl.Contains(prop.Name);

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
                            //Console.WriteLine("-----------------------Loan payment Test-------------------------");
                            bool checkJson = IsValidJson.IsValidJsonFormat(value.ToString());
                            if (alreadyExist)
                            {
                                //Console.WriteLine("After value: " + value.ToString());
                                //Console.WriteLine("Check Type : NOT Json Object policy \n");
                                /**************** Check Customer Date time and Convert ****************/
                                if (prop.Name == "loan_payment_date")
                                {
                                    value = DateTimeConvert.dateTimeConvert(value.ToString());

                                }
                                inputValue = value.ToString();
                                encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                attrInsurdes.SetValue(pocInsureds, encryptText);
                            }
                            else
                            {
                                attrInsurdes.SetValue(pocInsureds, value);
                            }
                        }
                        preItem = JsonConvert.SerializeObject(pocInsureds);
                        //jsonArrayEncrp.Add(preItem.ToString());
                        //Console.WriteLine("***************************** Loan payment Test ********************************");
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
