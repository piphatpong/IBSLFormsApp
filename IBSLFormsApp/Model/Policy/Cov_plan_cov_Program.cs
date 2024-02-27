using Newtonsoft.Json;
using IBSLFormsApp.Model.Libraries;
using System;
using System.Collections.Generic;
using System.Text;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using IBSLFormsApp.Model.MapDataPolicy;

namespace IBSLFormsApp.Model.Policy
{
    internal class Cov_plan_cov_Program
    {
        public static string cov_plan_cov_Program(List<string> sensitiveFieldList, List<string> listUrl, object jsonContent, string pubKeyCentralPath)
        {
            //string privKeyCentralPath = "ibsl.key";
            //string pubKeyCentralPath = "ibsl.crt";
            //string privKeycompPath = "company.key";
            //string pubKeycompPath = "company.crt";
            string inputValue;
            string encryptText = "";
            string preItem = "";
            string customJson = JsonConvert.SerializeObject(jsonContent);
            var jsonFormat = customJson.Replace(']', ' ').Substring(1);
            //Console.WriteLine("cov plan cov jsonContent : " + jsonFormat);
            Coverage_Plan_Coverage pocInsrdBene = JsonConvert.DeserializeObject<Coverage_Plan_Coverage>(jsonFormat);

            try
            {
                foreach (var prop in pocInsrdBene.GetType().GetProperties())
                {
                    var attr = pocInsrdBene.GetType().GetProperty(prop.Name);
                    var value = attr.GetValue(pocInsrdBene, null);

                    //Console.WriteLine("cov plan cov : " + prop.Name + " - " + value);

                    if (value == null || value is "[]")
                    {
                        encryptText = value == null ? null : "[]";
                        attr.SetValue(pocInsrdBene, encryptText);

                    }
                    else
                    {
                        /**************** Check Sensitive Field List *****************/
                        bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                        //Console.WriteLine("Sensitive: " + alreadyExist);
                        if (alreadyExist)
                        {
                            try
                            {
                                if (IsValidJson.IsValidJsonFormat(value.ToString()))
                                {
                                    //Console.WriteLine("Check Type : Json Object \n");
                                    //Customer pocInsrdBene = JsonConvert.DeserializeObject<Customer>(value.ToString());
                                    //Console.WriteLine(JsonConvert.SerializeObject(pocInsrdBene, serializerSettings));
                                }
                                else
                                {
                                    //Console.WriteLine("Cov plan cov prog");
                                    inputValue = value.ToString();
                                    encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                    //Console.WriteLine("Cov plan cov encryp: " + encryptText);
                                    attr.SetValue(pocInsrdBene, encryptText);
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Error!!!");
                            }
                        }
                        else
                        {
                            attr.SetValue(pocInsrdBene, value);
                        }

                    }

                    preItem = JsonConvert.SerializeObject(pocInsrdBene);
                    //Console.WriteLine("Cust Result: " + preItem);

                    //return(encryptText);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " Encrypt => Can't find public-key ibsl. \n");
            }

            return "[" + preItem + "]";
        }
    }
}
