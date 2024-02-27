using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.Libraries
{
    internal class DateTimeConvert
    {
        public static string dateTimeConvert(dynamic InputDate)
        {
            var InputStrDate = InputDate + "+7:00";
            //Console.WriteLine("ConvertDate: " + InputStrDate);
            return InputStrDate;
        }
    }
}
