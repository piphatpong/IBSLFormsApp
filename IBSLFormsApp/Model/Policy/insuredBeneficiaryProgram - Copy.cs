using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static IBSLFormsApp.Model.Libraries.IsValidJson;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using IBSLFormsApp.Model.MapDataPolicy;
using IBSLFormsApp.Model.Libraries;

namespace IBSLFormsApp.Model.Policy
{
    internal class insuredBeneficiary_Program_Copy
    {
        public static string insuredbeneficiary(List<string> sensitiveFieldList, object jsonContent, string pubKeyCentralPath)
        {
            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            string logTest = "insuredbeneficiary: " + jsonContent.ToString();
            debl.debuglog(logTest);

            string inputValue;
            string encryptText = "";
            string preItem = "";
            string customJson = JsonConvert.SerializeObject(jsonContent);
            var jsonFormat = customJson.Replace(']', ' ').Substring(1);
            //Console.WriteLine("InsBene jsonContent : " + jsonFormat);
            insrdBenefi pocInsrdBene = JsonConvert.DeserializeObject<insrdBenefi>(jsonFormat);

            try
            {
                foreach (var prop in pocInsrdBene.GetType().GetProperties())
                {
                    var attr = pocInsrdBene.GetType().GetProperty(prop.Name);
                    var value = attr.GetValue(pocInsrdBene, null);

                    //Console.WriteLine("InsBene propname : " + prop.Name + " - " + value);

                    if (value == null || value is "[]")
                    {
                        encryptText = value == null ? null : "[]";
                    }
                    else
                    {
                        /**************** Check Sensitive Field List *****************/
                        bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                        //Console.WriteLine("Sensitive: " +  alreadyExist);
                        if (alreadyExist)
                        {
                            try
                            {
                                if (IsValidJsonFormat(value.ToString()))
                                {
                                    //Console.WriteLine("Check Type : Json Object \n");
                                    //Customer pocInsrdBene = JsonConvert.DeserializeObject<Customer>(value.ToString());
                                    //Console.WriteLine(JsonConvert.SerializeObject(pocInsrdBene, serializerSettings));
                                }
                                else
                                {

                                    //Console.WriteLine("Insured benefi");
                                    inputValue = value.ToString();
                                    encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                    //Console.WriteLine("Insured bene encryp: " + encryptText);
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
