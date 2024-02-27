using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    internal class Policy_Coverage_Plan_Sequence
    {
        public int cov_plan_seq { get; set; }
        public string cov_plan_name { get; set; }
        public decimal cov_plan_sum_insured { get; set; }
        public decimal cov_plan_maturity_benefit { get; set; }
        public decimal cov_plan_survival_benefit_min { get; set; }
        public decimal cov_plan_survival_benefit_max { get; set; }
        public JArray coverages_under_plans { get; set; }
    }
}
