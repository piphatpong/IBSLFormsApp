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
    internal class insuredBeneficiary_Program
    {
        public static string insuredbeneficiary(List<string> sensitiveFieldList, object jsonContent, string pubKeyCentralPath)
        {
            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            //string logTest = "insuredbeneficiary: " + jsonContent.ToString();
            //debl.debuglog(logTest);

            string inputValue;
            string encryptText = "";
            string preItem = "";
            List<string> allEncryItem = new List<string>();
            string customJson = JsonConvert.SerializeObject(jsonContent);


            dynamic jsonarry = JsonConvert.DeserializeObject(customJson);
            try
            {
                foreach (var item in jsonarry)
                {
                    string itemConvert = JsonConvert.SerializeObject(item);

                    //debl.debuglog("insure-benefit: " + item);

                    insrdBenefi pocInsrdBene = JsonConvert.DeserializeObject<insrdBenefi>(itemConvert);
                
                    foreach (var prop in pocInsrdBene.GetType().GetProperties())
                    {
                        var attr = pocInsrdBene.GetType().GetProperty(prop.Name);
                        var value = attr.GetValue(pocInsrdBene, null);

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
                                if (prop.Name == "insrd_beneficiary_relation")
                                {
                                    attr.SetValue (pocInsrdBene, value.ToString().PadLeft(2,'0'));
                                }
                                else
                                {
                                    attr.SetValue(pocInsrdBene, value);
                                }
                            }

                        }
                        preItem = JsonConvert.SerializeObject(pocInsrdBene);
                    }// For loop //

                    allEncryItem.Add(preItem);
                }// For loop //
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex + " Encrypt => Can't find public-key ibsl. \n");
                debl.debuglog("Error Exception:" +  ex.Message);
            }
            string[] returnArray = allEncryItem.ToArray();
           
            preItem = null;
            foreach (var item in returnArray)
            {
                if (preItem is not null)
                {
                    preItem = preItem + ',' + item;
                }
                else
                {
                    preItem = preItem + item;
                }
            }
            preItem = "[" + preItem + "]";

            //debl.debuglog("Insr-Benefit return: " + preItem);

            return preItem;

        }

    }
}
