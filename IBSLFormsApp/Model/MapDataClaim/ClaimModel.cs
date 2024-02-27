using Newtonsoft.Json.Linq;
using System;

namespace IBSLFormsApp.Model.MapDataClaim
{
    public class ClaimModel
    {
      public string claim_refer_code_of_company { get; set; }
      public string claim_transaction_status { get; set; }
      public string claim_company_id { get; set; }
      public string claim_pol_id { get; set; }
      public string claim_pol_refer_code_of_company { get; set; }
      public string claim_id { get; set; }
      public string claim_no { get; set; }
      public string claim_type { get; set; }
      public string claim_pol_type { get; set; }
      public string claim_incurred_date { get; set; }
      public string claim_report_date { get; set; }
      public string claim_notify_date { get; set; }
      public string claim_discharge_date { get; set; }
      public string claim_hospital_id { get; set; }
      public string claim_hospital_name { get; set; }
      public string claim_doctor_name { get; set; }
      public string claim_completion_date { get; set; }
      public string claim_close_date { get; set; }
      public string claim_status { get; set; }
      public string claim_reject_reason { get; set; }
      public string claim_document_link { get; set; }
      public string claim_causes_of_death { get; set; }
      public string claim_oic_case_id { get; set; }
      public string claim_oic_case_detail { get; set; }
      public string claim_request_channel { get; set; }
      public JArray claim_insrd_ids { get; set; }
      public JArray claim_icd_10_codes { get; set; }
      public JArray claim_icd_9_codes { get; set; }
      public JArray claim_coverages { get; set; }
      public JArray estimates { get; set; }
      public JArray claim_payments { get; set; }

    }
}
