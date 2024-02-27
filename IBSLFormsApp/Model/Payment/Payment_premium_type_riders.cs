using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using System.Text.Json.Nodes;
using static IBSLFormsApp.Model.MapDataPayment.Payment_Premium_Type_Rider_Mapdata;
using IBSLFormsApp.Model.MapDataPayment;

namespace IBSLFormsApp.Model.Payment
{
    internal class Payment_premium_type_riders
    {
        public static string payment_premium_type_riders(List<string> sensitiveFieldList, object jsonContent, String pubKeyCert)
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

            //Console.WriteLine("Insure PaymentProg jsonContent : " + insuresdJson);
            dynamic jsonarry = JsonConvert.DeserializeObject(insuresdJson);
            //Console.WriteLine("Insure PaymentProg: " +  jsonarry.ToString());

            foreach (var item in jsonarry)
            {
                //Console.WriteLine("item1: " + item.ToString());

                insuresdJson = JsonConvert.SerializeObject(item);

                Payment_Premium_Type_Rider_Mapdata pocInsureds = JsonConvert.DeserializeObject<Payment_Premium_Type_Rider_Mapdata>(insuresdJson);

                //Console.WriteLine("item2: " + pocInsureds);

                try
                {
                    foreach (var prop in pocInsureds.GetType().GetProperties())
                    {
                        var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);
                        var value = attrInsurdes.GetValue(pocInsureds, null);

                        //Console.WriteLine("Coverages Test : " + prop.Name.Trim() + "-" + value + "\n");

                        if (value == null || value is "[]")
                        {
                            encryptText = value == null ? null : "[]";
                            attrInsurdes.SetValue(pocInsureds, encryptText);

                        }
                        else
                        {
                            /**************** Check Sensitive Field List *****************/
                            bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                            //Console.WriteLine("Sensitive: " +  alreadyExist);
                            //Console.WriteLine("-----------------------Coverage Test-------------------------");
                            if (alreadyExist)
                            {
                                //Console.WriteLine("After value: " + value.ToString());
                                //Console.WriteLine("Check Type : NOT Json Object policy \n");
                                inputValue = value.ToString();
                                encryptText = EncryptData(inputValue, pubKeyCert);
                                attrInsurdes.SetValue(pocInsureds, encryptText);
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
