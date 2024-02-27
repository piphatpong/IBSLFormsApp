using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static IBSLFormsApp.Model.Libraries.IsValidJson;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using static IBSLFormsApp.Model.Libraries.DateTimeConvert;
using static IBSLFormsApp.Model.Claim.Simb2;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using IBSLFormsApp.Model.Claim;
using IBSLFormsApp.Model.MapDataClaim;
using IBSLFormsApp.Model.Libraries;

namespace IBSLFormsApp.Model.Claim
{
    internal class Claim_coverages
    {
        public static string claimcoveragesfunc(List<string> sensitiveFieldList, object jsonContent, string pubKeyCert)
        {
            string encryptText = "";
            string preItem = "";
            string inputText = JsonConvert.SerializeObject(jsonContent);
            //JsonArray jsonArrayEncrp = new JsonArray();

            //Console.WriteLine("Insure Program jsonContent : " + insuresdJson);
            dynamic jsondearry = JsonConvert.DeserializeObject(inputText);
            //Console.WriteLine("Insure Program: " +  jsonarry.ToString());


            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            //debl.debuglog("Claim coverage: " + jsondearry);

            foreach (var item in jsondearry)
            {
                //Console.WriteLine("item1: " + item.ToString());
                //debl.debuglog("item claim cov: "+item.ToString());

                string claimcovJson = JsonConvert.SerializeObject(item);
                //debl.debuglog("item claim cov: " + claimcovJson);

                ClaimCoveragModel poc_seq = JsonConvert.DeserializeObject<ClaimCoveragModel>(claimcovJson);

                //Console.WriteLine("item2: " + poc_seq.ToString());
                //debl.debuglog("after item claim cov");

                try
                {
                    foreach (var prop in poc_seq.GetType().GetProperties())
                    {
                        var attrInsurdes = poc_seq.GetType().GetProperty(prop.Name);
                        var value = attrInsurdes.GetValue(poc_seq, null);
 
                        if (value == null)
                        {
                            encryptText = value == null ? null : "[]";
                            attrInsurdes.SetValue(poc_seq, encryptText);
                        }
                        else
                        {
                            /**************** Check Sensitive Field List *****************/
                            bool alreadyExist = sensitiveFieldList.Contains(prop.Name);

                            //debl.debuglog("Claim coverage sensitiveFieldList: " + alreadyExist);

                            bool checkJson = IsValidJsonFormat(value.ToString());

                            if (alreadyExist || checkJson)
                            {
                                //debl.debuglog("prop claim cov: " + prop.Name);
                                /**************************** Switch check More JsonObject *******************************/
                                switch (prop.Name.Trim())
                                {
                                    case "simb2s":
                                        string result1JArray = simb2s(sensitiveFieldList, value, pubKeyCert);
                                        try
                                        {
                                            if (value == null) {
                                                attrInsurdes.SetValue(poc_seq, "[]");
                                            }
                                            else
                                            {
                                                JArray result = JArray.Parse(result1JArray);
                                                attrInsurdes.SetValue(poc_seq, result);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            //Console.WriteLine("Error attr.SetValue" + ex);
                                            debl.debuglog("Error: " + ex.Message);
                                        }
                                        break;

                                    default:
                                        //Console.WriteLine("After value: " + value.ToString()+"\n");
                                        //Console.WriteLine("Check Type : NOT Json Object policy \n");
                                        //inputValue = value.ToString();
                                        //if (prop.Name.Trim() == "pmt_prd_premium_date" || prop.Name.Trim() == "pmt_prd_premium_due_date" ||
                                        //    prop.Name.Trim() == "pmt_prd_premium_temp_receipt_date" || prop.Name.Trim() == "pmt_prd_premium_receipt_date")
                                        //{
                                        //    value = dateTimeConvert(value);
                                            //Console.WriteLine("pmt_prd_premium_date :" + value);
                                        //}
                                        //debl.debuglog("Claim-Cov-default:"+prop.Name+" -> "+value);
                                        encryptText = EncryptData(value.ToString(), pubKeyCert);
                                        attrInsurdes.SetValue(poc_seq, encryptText);
                                        break;
                                }
                            }
                            else
                            {
                                attrInsurdes.SetValue(poc_seq, value);
                            }
                            //attrInsurdes.SetValue(poc_seq, encryptText);
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
