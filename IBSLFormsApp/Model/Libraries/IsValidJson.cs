using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.Libraries
{
    internal class IsValidJson
    {
        public static bool IsValidJsonFormat(string Input)
        {
            Input = Input.Trim();
            if (Input.StartsWith("{") && Input.EndsWith("}") || //For object
            Input.StartsWith("[") && Input.EndsWith("]")) //For array
            {
                try
                {
                    var obj = JToken.Parse(Input);
                    //Console.WriteLine("It's JSON OBject");
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    //Console.WriteLine("JEX: " + jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    //Console.WriteLine("EX: " + ex.ToString());
                    return false;
                }
            }
            else
            {
                //Console.WriteLine("It's not JSON OBject");
                return false;
            }
        }
    }
}
