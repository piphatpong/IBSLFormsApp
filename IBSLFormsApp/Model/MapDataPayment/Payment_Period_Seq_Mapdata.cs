using Newtonsoft.Json.Linq;

namespace IBSLFormsApp.Model.MapDataPayment
{
    public class Payment_Period_Seq_Mapdata
    {
      //public string pmt_id { get; set; }
      public string pmt_prd_premium_seq { get; set; }
      public string pmt_prd_premium_outstanding_payment { get; set; }
      public string pmt_prd_premium_amount { get; set; }
      public string pmt_prd_premium_amount_tax { get; set; }
      public string pmt_prd_premium_amount_life { get; set; }
      public string pmt_prd_premium_amt_saving { get; set; }
      public string pmt_prd_premium_amt_investment { get; set; }
      public string pmt_prd_premium_amount_other { get; set; }
      public string pmt_prd_premium_amount_com { get; set; }
      public string pmt_prd_premium_amt_interest { get; set; }
      public string pmt_prd_premium_date { get; set; }
      public string pmt_prd_premium_due_date { get; set; }
      public string pmt_prd_premium_temp_receipt_date { get; set; }
      public string pmt_prd_premium_receipt_date { get; set; }
      public string pmt_prd_premium_temp_receipt_number { get; set; }
      public string pmt_prd_premium_receipt_number { get; set; }
      public string pmt_prd_premium_channel { get; set; }
      public string pmt_prd_premium_channel_detail { get; set; }
      public JArray payment_premium_type_riders { get; set; }
      public JArray payment_premium_type_endorsements { get; set; }
    }
}
