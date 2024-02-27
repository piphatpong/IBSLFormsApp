using Newtonsoft.Json.Linq;
using System;

namespace IBSLFormsApp.Model.MapDataClaim
{
    public class ClaimPaymentModel
    {
      public string claim_payment_id { get; set; }
      public string claim_payment_type { get; set; }
      public int claim_payment_term { get; set; }
      public int claim_payment_term_paid { get; set; }
      public string claim_payment_channel { get; set; }
      public string claim_payment_date { get; set; }
      public string claim_payment_account_date { get; set; }
      public decimal claim_payment_invoice_amt { get; set; }
      public decimal claim_payment_amt { get; set; }
      public string claim_payment_other_invoice_amt { get; set; }
      public string claim_payment_beneficiary_name { get; set; }
    }
}
