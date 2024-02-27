using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.Policy
{
    internal class SensitiveFields
    {
        public Array SensFields()
        {
            string[] sensitiveField = new string[] {
                "pol_refer_code_of_company"
                ,"pol_company_id"
                ,"pol_id"
                ,"pol_start_date"
                ,"pol_end_date"
                ,"pol_payment_term"
                ,"pol_payment_amount"
                ,"pol_payment_period"
                ,"pol_info_url"
                ,"pol_claim_url"
                ,"pol_document_urls"
                ,"pol_year"
                ,"pol_status"
                ,"pol_status_note"
                ,"pol_status_date"
                ,"cust_type_other_detail"
                ,"cust_id"
                ,"cust_company_name"
                ,"cust_first_name"
                ,"cust_last_name"
                ,"cust_birthday"
                ,"cust_address"
                ,"cust_sub_district"
                ,"cust_district"
                ,"cust_province"
                ,"cust_zip_code"
                ,"cust_country_code"
                ,"cust_phone_number"
                ,"cust_mobile_number"
                ,"cust_email"
                ,"cust_address_permanent"
                ,"cust_sub_district_permanent"
                ,"cust_district_permanent"
                ,"cust_province_permanent"
                ,"cust_address_work"
                ,"cust_sub_district_work"
                ,"cust_district_work"
                ,"cust_province_work"
                ,"cust_address_current"
                ,"cust_sub_district_current"
                ,"cust_district_current"
                ,"cust_province_current"
                ,"insrd_id"
                ,"insrd_first_name"
                ,"insrd_last_name"
                ,"insrd_address"
                ,"insrd_sub_district"
                ,"insrd_district"
                ,"insrd_province"
                ,"insrd_zip_code"
                ,"insrd_country_code"
                ,"insrd_phone_number"
                ,"insrd_mobile_number"
                ,"insrd_email"
                ,"insrd_pol_sub_ids"
                ,"insrd_beneficiary_name"
                ,"sub_pol_id"
                ,"cov_plan_cov_code"
                ,"cov_plan_cov_detail"
                ,"cov_plan_cov_amt"
                ,"cov_code"
                ,"cov_detail"
                ,"cov_amt"
                ,"beneficiary_name"
                ,"endorse_detail"
                ,"loan_id"
                ,"loan_amount"
                ,"loan_payment_date"
                ,"loan_payment_amount"
                ,"loan_payment_interest"
                ,"loan_payment_principal"
                ,"inv_transaction_date"
                ,"inv_transaction_type"
                ,"inv_fund_unit"
                ,"inv_fund_nav"
                ,"inv_fund_thai_name"
                ,"inv_fund_eng_name"
                ,"inv_fund_type"
                ,"inv_fund_amc"


            };
            return sensitiveField;
        }

    }
}
