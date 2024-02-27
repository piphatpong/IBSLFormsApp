using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataClaim
{
    public class EditClaimModel
    {
        public EditClaimModel()
        {

        }
        public ClaimModel payload { get; set; }
        public string Signature { get; set; }
    }
}
