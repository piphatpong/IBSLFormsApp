using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.Claim
{
    internal class ClaimSensitiveFields
    {
        public Array SensFields()
        {
            string[] sensitiveField = new string[] {
                "claim_refer_code_of_company"
                ,"claim_company_id"
                ,"claim_pol_id"
                ,"claim_pol_refer_code_of_company"
                ,"claim_id"
                ,"claim_no"
            };
            return sensitiveField;
        }

    }
}
