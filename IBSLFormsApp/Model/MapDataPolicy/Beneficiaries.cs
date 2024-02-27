using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    internal class Beneficiaries
    {
        public int beneficiary_seq_no { get; set; }
        public string beneficiary_name { get; set; }
        public string beneficiary_relation { get; set; }
        public string beneficiary_relation_other_detail { get; set; }
        public string benefit_ratio { get; set; }
    }
}
