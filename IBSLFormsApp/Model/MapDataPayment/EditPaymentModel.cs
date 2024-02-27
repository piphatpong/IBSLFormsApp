using IBSLFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Model.MapDataPayment
{
    public class EditPaymentModel
    {
        public EditPaymentModel()
        {

        }
        public PaymentModel payload { get; set; }
        public string Signature { get; set; }
    }
}
