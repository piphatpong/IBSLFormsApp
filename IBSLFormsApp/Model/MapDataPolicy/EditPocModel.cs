using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPolicy
{
    public class EditPocModel
    {
        public EditPocModel()
        {

        }
        public PocModel payload { get; set; }
        public string Signature { get; set; }

    }
}
