using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using static IBSLFormsApp.Model.Libraries.DateTimeConvert;
using IBSLFormsApp.Model.Libraries;
using IBSLFormsApp.Model.MapDataPolicy;

namespace IBSLFormsApp.Model.Policy
{
    internal class Insrd_Pol_Sub_Ids_Prg
    {
        public static string insrd_pol_sub_ids_prg(List<string> sensitiveFieldList , object jsonContent, string pubKeyCentralPath)
        {
            DebugLog deblog = new DebugLog();
            string inputValue;
            string encryptText = "";
            string preItem = "";
            string resultJArray = "";
            string insuresdJson = JsonConvert.SerializeObject(jsonContent);
            JsonArray jsonArrayEncrp = new JsonArray();

            //Console.WriteLine("subPolicy PolicyProgram jsonContent : " + insuresdJson);
            dynamic jsonarry = JsonConvert.DeserializeObject(insuresdJson);
            //Console.WriteLine("Sub policy: " +  jsonarry.ToString());
            //deblog.debuglog("insrd_pol_sub_ids_prg : " + jsonContent.ToString());

            foreach (var item in jsonarry)
            {
                //deblog.debuglog("item2 : " + item);

                //Console.WriteLine("item1: " + item.ToString());

                insuresdJson = JsonConvert.SerializeObject(item);

                insrd_po_sub_policy pocInsureds = JsonConvert.DeserializeObject<insrd_po_sub_policy>(insuresdJson);

                //Console.WriteLine("item2: " + pocInsureds);
               

                /* try
                 { */
                foreach (var prop in pocInsureds.GetType().GetProperties())
                {
                    var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);
                    var value = attrInsurdes.GetValue(pocInsureds, null);

                    //Console.WriteLine("Sub pol Test : " + prop.Name.Trim() + "-" + value + "\n");

                    deblog.debuglog("insrd_pol_sub_ids_prg : " + attrInsurdes.ToString());

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
