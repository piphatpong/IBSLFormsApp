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
    public partial class FormStoreClaim : Form
    {
        public FormStoreClaim()
        {
            InitializeComponent();
        }

        private void FormStoreClaim_Load(object sender, EventArgs e)
        {

        }
        public string Claim_Store_query_stmt = @"
                        BEGIN
                            SELECT clm.[claim_refer_code_of_company]
                              ,clm.[claim_transaction_status]
                              ,clm.[claim_company_id]
                              ,clm.[claim_pol_id]
                              ,clm.[claim_pol_refer_code_of_company]
                              ,clm.[claim_id]
                              ,clm.[claim_no]
                              ,clm.[claim_type]
                              ,clm.[claim_pol_type]
                              ,clm.[claim_incurred_date]
                              ,clm.[claim_report_date]
                              ,clm.[claim_notify_date]
                              ,clm.[claim_discharge_date]
                              ,clm.[claim_hospital_id]
                              ,clm.[claim_hospital_name]
                              ,clm.[claim_doctor_name]
                              ,clm.[claim_completion_date]
                              ,clm.[claim_close_date]
                              ,clm.[claim_status]
                              ,clm.[claim_reject_reason]
                              ,clm.[claim_document_link]
                              ,clm.[claim_causes_of_death]
                              ,clm.[claim_oic_case_id]
                              ,clm.[claim_oic_case_detail]
                              ,clm.[claim_request_channel]
                              ,clm.[claim_insrd_ids]
                              ,clm.[claim_icd_10_codes]
                              ,clm.[claim_icd_9_codes]

                              ,/***** claim_coverages *****/
                              (select cov.[claim_coverage_code]
                                  ,cov.[claim_coverage_detail]
                                  ,cov.[claim_sum_insured_per_coverage_per_year]
                                  ,cov.[claim_sum_insured_per_coverage_per_time]
                                  ,cov.[claim_coverage_amt]
                                  ,cov.[claim_deductible_amt]
                                  ,cov.[claim_amount]

                                  ,/***** simb2s *****/
                                  (select sim.[claim_coverage_code_simb2]
                                         ,sim.[claim_pol_coverage_detail_simb2]
                                         ,sim.[claim_pol_coverage_detail_simb2_amt]
                                        FROM [Temp_SIMB2] sim
                                        WHERE sim.[claim_coverage_code_simb2] = cov.[simb2s]
                                        FOR JSON Auto, INCLUDE_NULL_VALUES) AS simb2s

                               FROM [Temp_Claim_Coverage] cov
                               FOR JSON Auto, INCLUDE_NULL_VALUES) AS claim_coverages

                              ,/***** estimates *****/
                              (select est.[claim_estimate_seq]
                                  ,est.[claim_estimate_date]
                                  ,est.[claim_estimate_amount]
                                  FROM [Temp_Estimate] est
                                  FOR JSON Auto, INCLUDE_NULL_VALUES) AS estimates
                              

                              ,/***** claim_payments *****/
                              (select clp.[claim_payment_id]
                                  ,clp.[claim_payment_type]
                                  ,clp.[claim_payment_term]
                                  ,clp.[claim_payment_term_paid]
                                  ,clp.[claim_payment_channel]
                                  ,clp.[claim_payment_date]
                                  ,clp.[claim_payment_account_date]
                                  ,clp.[claim_payment_invoice_amt]
                                  ,clp.[claim_payment_amt]
                                  ,clp.[claim_payment_other_invoice_amt]
                                  ,clp.[claim_payment_beneficiary_name]
                                                                    
                                   FROM [Temp_Claim_Payment] as clp
                                   WHERE clp.[claim_payment_id] = clm.[claim_payments]
                                   FOR JSON Auto, INCLUDE_NULL_VALUES) AS claim_payments

                      FROM [IBS_Life].[dbo].[Temp_Claim] clm                  
                      FOR JSON PATH, INCLUDE_NULL_VALUES

                END
           ";

        private void button1_Click(object sender, EventArgs e)
        {
            ClaimStoreProcModel claimStorePoc = new ClaimStoreProcModel();
            string query = "CREATE PROCEDURE " + ClaimStoreProc.Text.ToString() + " AS " + richBoxClaim.Text.ToString();
            string result = claimStorePoc.claimstoreprocmodel(query);
            MessageBox.Show(result);
        }

        private void richBoxClaim_TextChanged(object sender, EventArgs e)
        {

        }

        private void richBoxClaim_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
