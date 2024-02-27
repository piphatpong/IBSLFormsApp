using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace IBSLFormsApp.Model.MapDataPolicy
{
    public class PolicyModel
    {
        public string Pol_refer_code_of_company { get; set; }
        public string Tsource { get; set; }
        public string Pol_transaction_status { get; set; }
        public string Pol_company_id { get; set; }
        public string Org_polno { get; set; }
        public string Pol_id { get; set; }
        public string Org_poltype { get; set; }
        public string Pol_type { get; set; }
        public string Pol_type_gli_name { get; set; }
        public string Pol_product_type { get; set; }
        public string Pol_product_type_other_detail { get; set; }

        /*---- column 12 ----*/

    }
}
