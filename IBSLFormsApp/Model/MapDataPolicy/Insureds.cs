using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    public class InsuredsMaps
    {
        public int insrd_cov_plan_seq { get; set; }
        public int insrd_seq { get; set; }
        public string insrd_type { get; set; }
        public string insrd_id_type { get; set; }
        public string insrd_id { get; set; }
        public string insrd_first_name { get; set; }
        public string insrd_last_name { get; set; }
        public int insrd_age { get; set; }
        public string insrd_birthday { get; set; }
        public string insrd_gender { get; set; }
        public string insrd_address { get; set; }
        public string insrd_sub_district { get; set; }
        public string insrd_district { get; set; }
        public string insrd_province { get; set; }
        public string insrd_zip_code { get; set; }
        public string insrd_country_code { get; set; }
        public string insrd_phone_number { get; set; }
        public string insrd_mobile_number { get; set; }
        public string insrd_email { get; set; }
        public int insrd_issued_age { get; set; }
        public string insrd_race { get; set; }
        public string insrd_nationality { get; set; }
        public string insrd_religion { get; set; }
        public string insrd_occupation_level { get; set; }
        public string insrd_occupation_level_other_detail { get; set; }
        public string insrd_occupation { get; set; }
        public string insrd_job { get; set; }
        public string insrd_position { get; set; }
        public string insrd_business_type { get; set; }
        public string insrd_income_per_year { get; set; }
        public JArray insrd_pol_sub_ids { get; set; }
        public JArray insrd_beneficiaries { get; set; }

    }
}
