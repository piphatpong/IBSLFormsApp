﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static IBSLFormsApp.Model.Libraries.EncrypData;
using static IBSLFormsApp.Model.Libraries.DateTimeConvert;
using System.Text.Json.Nodes;
using IBSLFormsApp.Model.MapDataClaim;
using System.Linq;
using IBSLFormsApp.Model.Libraries;

namespace IBSLFormsApp.Model.Claim
{
    internal class ClaimPaymentPrg
    {
        public static string claimpaymentfunc(List<string> sensitiveFieldList, object jsonContent, string pubKeyCert)
        {
            //string privKeyCentralPath = "ibsl.key";
            //string pubKeyCentralPath = "ibsl.crt";
            //string privKeycompPath = "company.key";
            //string pubKeycompPath = "company.crt";

            /*************** Log Test ***************/
            //string logTest = "Count sql: " + reader.FieldCount + " store proc:" + storeProc;
            DebugLog debl = new DebugLog();
            string logTest = "";

            string inputValue;
            string encryptText = "";
            string preItem = "";
            string insuresdJson = JsonConvert.SerializeObject(jsonContent);
            JsonArray jsonArrayEncrp = new JsonArray();

            //Console.WriteLine("Insure Program jsonContent : " + insuresdJson);
            dynamic jsonarry = JsonConvert.DeserializeObject(insuresdJson);
            //Console.WriteLine("Claim_payment: " +  jsonarry.ToString());

            foreach (var item in jsonarry)
            {
                //Console.WriteLine("item1: " + item.ToString());
                //debl.debuglog("claim_payments item: " + item.ToString);

                insuresdJson = JsonConvert.SerializeObject(item);

                ClaimPaymentModel pocInsureds = JsonConvert.DeserializeObject<ClaimPaymentModel>(insuresdJson);

                //Console.WriteLine("item2: " + pocInsureds);

                try
                {
                    foreach (var prop in pocInsureds.GetType().GetProperties())
                    {
                        var attrInsurdes = pocInsureds.GetType().GetProperty(prop.Name);

                        var value = attrInsurdes.GetValue(pocInsureds, null);

                        //Console.WriteLine("Estimate Test : " + prop.Name.Trim() + "-" + value + "\n");
                        //debl.debuglog("claim_payment_date: " + prop.Name);

                        if (value == null || value is "[]")
                        {
                            encryptText = value == null ? null : "[]";
                            attrInsurdes.SetValue(pocInsureds, encryptText);
                        }
                        else
                        {
                            if (new[] {"claim_payment_date","claim_payment_account_date"}.Contains(prop.Name))
                            {
                                value = dateTimeConvert(value.ToString());
                            }

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