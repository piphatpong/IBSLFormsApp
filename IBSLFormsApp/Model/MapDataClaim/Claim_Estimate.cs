using Newtonsoft.Json.Linq;
using System;

namespace IBSLFormsApp.Model.MapDataClaim
{
    public class ClaimEstimate
    {
      public int claim_estimate_seq { get; set; }
      public string claim_estimate_date { get; set; }
      public decimal claim_estimate_amount { get; set; }
    }
}
