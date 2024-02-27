using Newtonsoft.Json.Linq;
using System;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    public class PocModel_Copy
    {
        public string pol_refer_code_of_company { get; set; }
        public string pol_transaction_status { get; set; }
        public string pol_company_id { get; set; }
        // public string Org_polno { get; set; }
        public string pol_id { get; set; }
        public string pol_type { get; set; }
        public int pol_type_gli_name { get; set; }
        public int pol_type_gli_plan { get; set; }
        public string pol_product_type { get; set; }
        public string pol_product_type_other_detail { get; set; }
        public string pol_type_2 { get; set; }
        public string pol_sub_type { get; set; }
        public string pol_name { get; set; }
        public string pol_name_oic { get; set; }
        // public string org_approvedate { get; set; }
        public string pol_start_date { get; set; }
        public string pol_end_date { get; set; }
        public string pol_claim_document { get; set; }
        public string pol_payment_term { get; set; }
        public decimal pol_payment_amount { get; set; }
        public string pol_payment_period { get; set; }
        public string pol_info_url { get; set; }
        public string pol_claim_url { get; set; }
        //---- Type Array ----//
        public string pol_document_urls { get; set; }
        public int pol_year { get; set; }
        public string pol_status { get; set; }
        public string pol_status_note { get; set; }
        public string pol_status_date { get; set; }
        public decimal pol_expropriation_value { get; set; }
        public decimal pol_immediate_refund { get; set; }
        public string pol_sale_date { get; set; }
        public string pol_transaction_date { get; set; }
        public string pol_approved_date { get; set; }
        public string pol_issue_date { get; set; }
        public string pol_account_date { get; set; }
        public string pol_agree_date { get; set; }
        public string pol_distribution { get; set; }
        public string pol_distribution_other_detail { get; set; }
        public string pol_license { get; set; }
        public string pol_re_insured_contact_number { get; set; }
        public string pol_life_detail { get; set; }
        public JObject customer { get; set; }
        public JArray insureds { get; set; }
        public JArray sub_policies { get; set; }
        public JArray beneficiaries { get; set; }
        public JArray endorsements { get; set; }
        public JArray loans { get; set; }
        public JArray loan_payments { get; set; }
        public JArray investments { get; set; }
    }
}
