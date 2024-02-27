using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using static IBSLFormsApp.Model.Libraries.IsValidJson;
using IBSLFormsApp.Model.MapDataPolicy;
using IBSLFormsApp.Model.Libraries;

namespace IBSLFormsApp.Model.Policy
{
    internal class BeneficiariesProgram
    {
        public static string beneficiariesProgram(List<string> sensitiveFieldList, List<string> listUrl, object jsonContent ,string pubKeyCentralPath)
        {
            //string privKeyCentralPath = "ibsl.key";
            //string pubKeyCentralPath = "ibsl.crt";
            //string privKeycompPath = "company.key";
            //string pubKeycompPath = "company.crt";
            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            string inputValue;
            string encryptText = "";
            string preItem = "";
            string insuresdJson = JsonConvert.SerializeObject(jsonContent);
            //JsonArray jsonArrayEncrp = new JsonArray();

            //Console.WriteLine("Insure PolicyProgram jsonContent : " + insuresdJson);
            

            dynamic jsonarry = JsonConvert.DeserializeObject(insuresdJson);
            //Console.WriteLine("Insure PolicyProgram: " +  jsonarry.ToString());

            foreach (var item in jsonarry)
            {
                //Console.WriteLine("item1: " + item.ToString());

                insuresdJson = JsonConvert.SerializeObject(item);
                
                //debl.debuglog("Ben-in: " + insuresdJson);

                Beneficiaries pocInsureds = JsonConvert.DeserializeObject<Beneficiaries>(insuresdJson);

                //debl.debuglog("Ben-out: " + pocInsureds.ToString());
                //Console.WriteLine("item2: " + pocInsureds);

                try
                {
                    foreach (var prop in pocInsureds.GetType().GetProperties())
                    {
                        var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);

                        var value = attrInsurdes.GetValue(pocInsureds, null);

                        //Console.WriteLine("Beneficiaries prop : " + prop.Name.Trim() + "-" + value);

                        //bool results = listUrl.Contains(prop.Name);

                        //debl.debuglog("BenC name: "+ prop.Name+" value: "+value);

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
                            //Console.WriteLine("-----------------------Beneficiaries Test-------------------------");
                            //bool checkJson = IsValidJson.IsValidJsonFormat(value.ToString());
                            if (alreadyExist)
                            {
                                //Console.WriteLine("After value: " + value.ToString());
                                //Console.WriteLine("Check Type : NOT Json Object policy \n");
                                inputValue = value.ToString();
                                encryptText = EncryptData(inputValue, pubKeyCentralPath);
                                attrInsurdes.SetValue(pocInsureds, encryptText);
                                //debl.debuglog("Encry BenC name: " + prop.Name + " value: " + encryptText);
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
