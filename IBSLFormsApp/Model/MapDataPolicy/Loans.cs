using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    internal class Loans
    {
        public string loan_id { get; set; }
        public string loan_type { get; set; }
        public string loan_date { get; set; }
        public decimal loan_interest_rate { get; set; }
        public string loan_amount { get; set; }
        public decimal loan_interest_amount { get; set; }
        public string loan_status { get; set; }
    }
}
