using IBSLFormsApp.Model.Libraries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBSLFormsApp.Model.Policy
{
    internal class CheckInsBen
    {
        public static JObject checkInsBen(JObject item)
        {
            bool Null_InsBen = true;

            bool Null_Bene = true;
            
            string Return_Signal = null;

            DebugLog debl = new DebugLog();

            string insurString = JsonConvert.SerializeObject(item["insureds"]);

            //debl.debuglog("item1 : " + insurString);

            JArray insurJobject = JsonConvert.DeserializeObject<JArray>(insurString);

            if (insurJobject is not null && insurJobject[0]["insrd_beneficiaries"].ToString() == "[]")
            {
                Null_InsBen = true;
            }
            else { Null_InsBen = false; }

            //debl.debuglog("insrd_beneficiaries : " + insurJobject[0]["insrd_beneficiaries"].ToString() + " bool :" + Null_InsBen.ToString());

            if (item["beneficiaries"].ToString() == "")
            {
                Null_Bene = true;
            }
            else { Null_Bene = false; }

            //debl.debuglog("benefitciaries: " + item["beneficiaries"].ToString() + " bool :" + Null_Bene.ToString());

            if (Null_InsBen && Null_Bene) 
            { 
                item = null;
                Return_Signal = "cross"; }
            else if (!Null_InsBen)
            {
                Return_Signal = "InsBen";
                item["beneficiaries"] = null;
            }else if (!Null_Bene) 
                { Return_Signal = "Bene"; }


            debl.debuglog("InsBen-Chk: " + item.ToString());

            return item;
        }
    }
}