using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataClaim
{
    public class MapClaimModel
    {
        public MapClaimModel()
        {

        }
        public JObject Body { get; set; }
        public JObject Response { get; set; }
    }
}
