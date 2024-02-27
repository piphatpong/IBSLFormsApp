using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.Payment
{
    internal class PaymentSensitiveFields
    {
        public Array PaymentSensFields()
        {
            string[] sensitiveField = new string[] {
                "pmt_pol_id"
                ,"pmt_pol_refer_code_of_company"
                ,"pmt_id"
                ,"pmt_premium_payment_period_year"
                ,"pmt_payment_period"
                ,"pmt_prd_premium_seq"
                ,"pmt_prd_premium_outstanding_payment"
                ,"pmt_prd_premium_amount"
                ,"pmt_prd_premium_amount_tax"
                ,"pmt_prd_premium_amount_life"
                ,"pmt_prd_premium_amt_saving"
                ,"pmt_prd_premium_amt_investment"
                ,"pmt_prd_premium_amount_other"
                ,"pmt_prd_premium_amount_com"
                ,"pmt_prd_premium_amt_interest"
                ,"pmt_prd_premium_date"
                ,"pmt_prd_premium_due_date"
                ,"pmt_prd_premium_temp_receipt_date"
                ,"pmt_prd_premium_receipt_date"
                ,"pmt_prd_premium_receipt_number"
                ,"pmt_prd_premium_temp_receipt_number"
                ,"pmt_prd_premium_channel"
                ,"pmt_prd_premium_channel_detail"
                ,"pmt_premium_rid_amount"
                ,"pmt_premium_edm_amount"
            };
            return sensitiveField;
        }

    }
}
