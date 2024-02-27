using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    internal class Coverage_Plan_Coverage
    {
        public string cov_plan_cov_code { get; set; }
        public string cov_plan_cov_detail { get; set; }
        public string cov_plan_cov_amt { get; set; }
        public decimal cov_plan_cov_sum_insured_per_coverage_per_year { get; set; }
        public decimal cov_plan_cov_sum_insured_per_coverage_per_time { get; set; }
        public decimal cov_plan_cov_co_payment { get; set; }
        public decimal cov_plan_cov_deductible_amount { get; set; }
        public string cov_plan_cov_deductible_detail { get; set; }
        public int cov_plan_cov_number_of_day { get; set; }
        public int cov_plan_cov_cover_year { get; set; }
        public int cov_plan_cov_payment_year { get; set; }
        public string cov_plan_cov_claim_status { get; set; }
    }
}
