using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static IBSLFormsApp.Model.Libraries.DateTimeConvert;
using static IBSLFormsApp.Model.Libraries.IsValidJson;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using IBSLFormsApp.Model.MapDataPolicy;
using IBSLFormsApp.Model.Libraries;

namespace IBSLFormsApp.Model.Policy
{
    internal class CustomerProgram
    {
        public static string customerProgram(List<string> sensitiveFieldList, object jsonContent, string pubKeyCentralPath)
        {
            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            //string logTest = "CCCustomer: "+ jsonContent.ToString();
            //debl.debuglog(logTest);

            string inputValue;
            string encryptText = "";
            string preItem = "";
            string customJson = JsonConvert.SerializeObject(jsonContent);
            
            //dynamic jsonarry = JsonConvert.DeserializeObject(customJson);

            //debl.debuglog("Customer Json:" + customJson);
            //debl.debuglog("Cust convert text:" + jsonarry);


            //String custJson = customJson[0].ToString();
            //debl.debuglog("Cust Text:" + custJson);

            Customer pocCustomer = JsonConvert.DeserializeObject<Customer>(customJson);
            //logTest = "test2 test2 test2";
            //debl.debuglog("poc: " + pocCustomer);


           // dynamic jsonarry = JsonConvert.DeserializeObject(customJson);

            try
            {
                foreach (var prop in pocCustomer.GetType().GetProperties())
                {
                    var attr = pocCustomer.GetType().GetProperty(prop.Name);
                    var value = attr.GetValue(pocCustomer, null);

                    //logTest = "Customer-: " + attr;
                    //debl.debuglog(logTest);

                    /**************** Check Customer Date time and Convert ****************/
                    if (prop.Name == "cust_birthday" && value != null)
                    {
                        value = dateTimeConvert(value.ToString());

                    }

                    /**************** Check Customer Cust_province ****************/
                    if (prop.Name == "cust_province" && value.ToString().Length > 2)
                    {
                        int countStr = value.ToString().Length - 1;

                        Console.WriteLine(value.ToString() + "/" + countStr);

                        value = value.ToString().Substring(0,2);

                        Console.WriteLine(value.ToString() );
                    }

                    /**************** Check Customer Country code and Convert ****************/
                    if (prop.Name == "cust_country_code" && value != null)
                    {
                        switch (value)
                        {
                            case "TH":
                                value = "THA";
                                break;

                            default:
                                break;
                        };
                    }

                    /**************** Check Sensitive Field List *****************/
                    bool alreadyExist = sensitiveFieldList.Contains(prop.Name);

                    //Console.WriteLine("customer: "+ prop.Name+" = "+value);

                    if (alreadyExist && value != null)
                    {

                        if (IsValidJsonFormat(value.ToString()))
                        {
                            //Console.WriteLine("Check Type : Json Object \n");
                            //Customer pocCustomer = JsonConvert.DeserializeObject<Customer>(value.ToString());
                            //Console.WriteLine(JsonConvert.SerializeObject(pocCustomer, serializerSettings));
                        }
                        else
                        {
                            //Console.WriteLine("Check Type : NOT Json Object customer \n");
                            inputValue = value.ToString();
                            encryptText = EncryptData(inputValue, pubKeyCentralPath);
                            //Console.WriteLine("Customer encryp: " + encryptText);

                            attr.SetValue(pocCustomer, encryptText);
                        }

                    }
                    else
                    {
                        //********* Test change customer type to fix value "P" **********//
                        if (prop.Name == "cust_type")
                        {
                            value = "P";
                        }

                        attr.SetValue(pocCustomer, value);
                    }

                    preItem = JsonConvert.SerializeObject(pocCustomer);
                    //Console.WriteLine("Cust Result: " + preItem);

                    //return(encryptText);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex + " Encrypt => Can't find public-key ibsl. \n");
                debl.debuglog("Err Exception: " + ex.Message);

            }

            //debl.debuglog("Cust Res:" + preItem);

            return preItem;
        }
    }
}
