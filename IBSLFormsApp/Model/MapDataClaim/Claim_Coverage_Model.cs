using Newtonsoft.Json.Linq;
using System;

namespace IBSLFormsApp.Model.MapDataClaim
{
    public class ClaimCoveragModel
    {
      public string claim_coverage_code { get; set; }
      public string claim_coverage_detail { get; set; }
      public string claim_sum_insured_per_coverage_per_year { get; set; }
      public string claim_sum_insured_per_coverage_per_time { get; set; }
      public string claim_coverage_amt { get; set; }
      public string claim_deductible_amt { get; set; }
      public string claim_amount { get; set; }
      public JArray simb2s { get; set; }
    }
}
