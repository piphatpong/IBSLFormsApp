using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    public class MapPocLog
    {
        public MapPocLog()
        {

        }
        public JObject Body { get; set; }
        public JObject Response { get; set; }

    }
}
