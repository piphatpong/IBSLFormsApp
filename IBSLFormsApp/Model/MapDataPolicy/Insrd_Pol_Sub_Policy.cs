using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    public class insrd_po_sub_policy
    {
        public string sub_pol_id { get; set; }
        public string sub_pol_type { get; set; }
        public string sub_pol_ppt { get; set; }
        public string sub_pol_start_date { get; set; }
        public string sub_pol_end_date { get; set; }
        public string sub_pol_sum_insured_per_year_exclude { get; set; }
        public string sub_pol_sum_insured_per_year_include { get; set; }
        public string policy_cov_plan_seqs { get; set; }
        public string coverages { get; set; }
    }
}
