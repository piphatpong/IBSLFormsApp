
using Newtonsoft.Json.Linq;
using System;

namespace IBSLFormsApp
{
    public class PaymentModel
    {
        public string pmt_refer_code_of_company { get; set; }
        public string pmt_transaction_status { get; set; }
        public string pmt_company_id { get; set; }
        public string pmt_pol_id { get; set; }
        public string pmt_pol_refer_code_of_company { get; set; }
        public string pmt_id { get; set; }
        public string pmt_type { get; set; }
        public string pmt_direct_premium { get; set; }
        public string pmt_premium_payment_period_year { get; set; }
        public int pmt_premium_payment_year { get; set; }
        public string pmt_payment_period { get; set; }
        public JArray payment_period_seqs { get; set; }
        
    }
}
