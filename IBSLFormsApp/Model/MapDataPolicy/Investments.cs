using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    internal class Investments
    {
        public string inv_transaction_date { get; set; }
        public string inv_transaction_type { get; set; }
        public JArray policy_fund_codes { get; set; }
    }
}
