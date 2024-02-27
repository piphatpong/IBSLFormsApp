using IBSLFormsApp.Model;
using Newtonsoft.Json.Linq;

namespace IBSLFormsApp.Model.MapDataPayment
{
    public class MapPayModel
    {
        public MapPayModel()
        {

        }
        public JObject Body { get; set; }
        public JObject Response { get; set; }
    }
}
