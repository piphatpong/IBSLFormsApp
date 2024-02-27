using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    public class Coverages
    {
        public string cov_code { get; set; }
        public string cov_detail { get; set; }
        public string cov_amt { get; set; }
        public string cov_sum_insured_per_coverage_per_year { get; set; }
        public string cov_sum_insured_per_coverage_per_time { get; set; }
        public string cov_co_payment { get; set; }
        public string cov_deductible_amount { get; set; }
        public string cov_deductible_detail { get; set; }
        public string cov_number_of_day { get; set; }
        public int cov_cover_year { get; set; }
        public int cov_payment_year { get; set; }
        public string cov_claim_status { get; set; }
    }
}
