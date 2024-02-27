using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static IBSLFormsApp.Model.Libraries.IsValidJson;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using static IBSLFormsApp.Model.Payment.Payment_premium_type_riders;
using static IBSLFormsApp.Model.Payment.Payment_premium_type_endorsement;
using static IBSLFormsApp.Model.Libraries.DateTimeConvert;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using IBSLFormsApp.Model.MapDataPayment;

namespace IBSLFormsApp.Model.Payment
{
    internal class Payment_Period_Seqs
    {
        public static string payment_period_seqs(List<string> sensitiveFieldList, object jsonContent, String pubKeyCert)
        {
            //string privKeyCentralPath = "ibsl.key";
            //string pubKeyCentralPath = "ibsl.crt";
            //string privKeycompPath = "company.key";
            //string pubKeycompPath = "company.crt";
            //string inputValue;
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

                Payment_Period_Seq_Mapdata poc_seq = JsonConvert.DeserializeObject<Payment_Period_Seq_Mapdata>(insuresdJson);

                //Console.WriteLine("item2: " + poc_seq.ToString());

                try
                {
                    foreach (var prop in poc_seq.GetType().GetProperties())
                    {
                        var attrInsurdes = poc_seq.GetType().GetProperty(prop.Name);
                        var value = attrInsurdes.GetValue(poc_seq, null);

                        //Console.WriteLine("Seqs Test : " + prop.Name.Trim() + "-" + value + "\n");

                        if (value == null || value is "[]")
                        {
                            encryptText = value == null ? null : "[]";
                        }
                        else
                        {
                            /**************** Check Sensitive Field List *****************/
                            bool alreadyExist = sensitiveFieldList.Contains(prop.Name);
                            //Console.WriteLine("Sensitive: " +  alreadyExist);
                            //Console.WriteLine("-----------------------Insureds Test-------------------------");
                            //Console.WriteLine(value.ToString());
                            bool checkJson = IsValidJsonFormat(value.ToString());
                            if (alreadyExist || checkJson)
                            {
                                /**************************** Switch check More JsonObject *******************************/
                                switch (prop.Name.Trim())
                                {
                                    case "payment_premium_type_riders":
                                        string result1JArray = payment_premium_type_riders(sensitiveFieldList, value, pubKeyCert);
                                        try
                                        {
                                            JArray result = JArray.Parse(result1JArray);
                                            attrInsurdes.SetValue(poc_seq, result);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Error attr.SetValue" + ex);
                                        }
                                        break;

                                    case "payment_premium_type_endorsements":
                                        //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
                                        //Console.WriteLine("Before InsuredsProg value: " + value.ToString());

                                        string result2JArray = payment_premium_type_endorsement(sensitiveFieldList, value, pubKeyCert);
                                        try
                                        {
                                            JArray result = JArray.Parse(result2JArray);
                                            attrInsurdes.SetValue(poc_seq, result);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Error attr.SetValue" + ex);
                                        }
                                        break;

                                    default:
                                        //Console.WriteLine("After value: " + value.ToString()+"\n");
                                        //Console.WriteLine("Check Type : NOT Json Object policy \n");
                                        //inputValue = value.ToString();
                                        if (prop.Name.Trim() == "pmt_prd_premium_date" || prop.Name.Trim() == "pmt_prd_premium_due_date" || 
                                            prop.Name.Trim() == "pmt_prd_premium_temp_receipt_date" || prop.Name.Trim() == "pmt_prd_premium_receipt_date")
                                        {
                                            value = dateTimeConvert(value);
                                            //Console.WriteLine("pmt_prd_premium_date :" + value);
                                        }
                                        encryptText = EncryptData(value.ToString(), pubKeyCert);
                                        attrInsurdes.SetValue(poc_seq, encryptText);
                                        break;
                                }

                            }
                            else
                            {
                                
                                attrInsurdes.SetValue(poc_seq, value);
                            }
                        }
                        preItem = JsonConvert.SerializeObject(poc_seq);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex + "Encrypt => Can't find public-key ibsl. \n");
                }
            }

            //jsonArrayEncrp.Add(preItem.Substring(1));
            preItem = "[" + preItem + "]";

            //Console.WriteLine("jsonArrayEncrp.ToJsonString(): " + preItem);

            return preItem;
        }
    }
}
