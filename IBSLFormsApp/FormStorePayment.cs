using IBSLFormsApp.StorePoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBSLFormsApp
{
    public partial class FormStorePayment : Form
    {
        public FormStorePayment()
        {
            InitializeComponent();
        }
        public string Payment_Store_query_stmt = @"SELECT pmt.[pmt_refer_code_of_company]
                              ,pmt.[pmt_transaction_status]
                              ,pmt.[pmt_company_id]
                              ,pmt.[pmt_pol_id]
                              ,pmt.[pmt_pol_refer_code_of_company]
                              ,pmt.[pmt_id]
                              
                              ,right('00' + cast(pmt.pmt_type as varchar(2)), 2) as pmt_type
                              ,pmt.[pmt_direct_premium]
                              ,pmt.[pmt_premium_payment_period_year]
                              ,pmt.[pmt_premium_payment_year]
                              ,pmt.[pmt_payment_period]

                          ,/***** payment_period_seqs *****/
                           (select seq.[pmt_refer_code_of_company] 
                                  ,seq.[pmt_prd_premium_seq]
                                  ,seq.[pmt_prd_premium_outstanding_payment]
                                  ,seq.[pmt_prd_premium_amount]
                                  ,seq.[pmt_prd_premium_amount_tax]
                                  ,seq.[pmt_prd_premium_amount_life]
                                  ,seq.[pmt_prd_premium_amt_saving]
                                  ,seq.[pmt_prd_premium_amt_investment]
                                  ,seq.[pmt_prd_premium_amount_other]
                                  ,seq.[pmt_prd_premium_amount_com]
                                  ,seq.[pmt_prd_premium_amt_interest]
                                  ,seq.[pmt_prd_premium_date]
                                  ,seq.[pmt_prd_premium_due_date]
                                  ,seq.[pmt_prd_premium_temp_receipt_date]
                                  ,seq.[pmt_prd_premium_receipt_date]
                                  ,seq.[pmt_prd_premium_temp_receipt_number]
                                  ,seq.[pmt_prd_premium_receipt_number]
                                  ,seq.[pmt_prd_premium_channel]
                                  ,seq.[pmt_prd_premium_channel_detail]
                                  
                                  ,/*********** payment_premium_type_riders **********/
                                  (select rid.[pmt_premium_rid_number]
                                        ,rid.[pmt_premium_rid_amount]

                                   FROM Temp_payment_premium_type_riders as rid
                                   WHERE rid.[pmt_premium_rid_number] = seq.[pmt_refer_code_of_company]
                                   FOR JSON Auto, INCLUDE_NULL_VALUES) AS payment_premium_type_riders

                                  ,/*********** payment_premium_type_endorsements **********/
                                  (select eds.[pmt_premium_edm_number]
                                        , eds.[pmt_premium_edm_amount]

                                   FROM Temp_payment_premium_type_endorsements as eds
                                   WHERE eds.[pmt_premium_edm_number] = seq.[pmt_refer_code_of_company]
                                   FOR JSON Auto, INCLUDE_NULL_VALUES) AS payment_premium_type_endorsements

                              FROM Temp_Payment_period_seqs_IBSL as seq
                              WHERE pmt.[pmt_refer_code_of_company] = seq.[pmt_refer_code_of_company]
                              FOR JSON Auto, INCLUDE_NULL_VALUES) AS payment_period_seqs


                  FROM Temp_Payment_27_2_67 pmt

                  INNER join Temp_policy_27_2_67 pol
                  
                  ON pmt.pmt_pol_refer_code_of_company = pol.pol_refer_code_of_company

                  FOR JSON PATH, INCLUDE_NULL_VALUES


                 END
                ";

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonRunPayment_Click(object sender, EventArgs e)
        {
            PaymentStoreProcModel paymentStorePoc = new PaymentStoreProcModel();
            string query = "CREATE PROCEDURE " + fileStoreProc.Text.ToString() + " AS \r\n BEGIN \r\n" + richTextPayment.Text.ToString();
            string result = paymentStorePoc.paymentstoreprocmodel(query);
            MessageBox.Show(result);
        }

        private void richTextPayment_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormStorePayment_Load(object sender, EventArgs e)
        {

        }
    }
}
