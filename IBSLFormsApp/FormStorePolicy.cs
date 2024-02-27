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
    public partial class FormStorePolicy : Form
    {
        public FormStorePolicy()
        {
            InitializeComponent();
        }
        private void groupBoxPolicy_Enter(object sender, EventArgs e)
        {

        }

        public string pol_Store_query_stmt =
                 @"BEGIN

                        SELECT pol.ref_seq 
                                ,pol.[pol_refer_code_of_company]
                                ,pol.[pol_transaction_status]
                                ,pol.[pol_company_id]
                                ,pol.[pol_id]
                                ,pol.[pol_type]
                                ,pol.[pol_type_gli_name]
                                ,pol.[pol_type_gli_plan]
                                ,pol.[pol_product_type]
                                ,pol.[pol_product_type_other_detail]
                                ,pol.[pol_type_2]
                                ,pol.[pol_sub_type]
                                ,pol.[pol_name]
                                ,pol.[pol_name_oic]
                                ,pol.[pol_start_date]
                                ,pol.[pol_end_date]
                                ,pol.[pol_claim_document]
                                ,pol.[pol_payment_term]
                                ,pol.[pol_payment_amount]
                                ,pol.[pol_payment_period]
                                ,pol.[pol_info_url]
                                ,pol.[pol_claim_url]
                                ,pol.[pol_document_urls]
                                ,pol.[pol_year]
                                ,pol.[pol_status]
                                ,pol.[pol_status_note]
                                ,pol.[pol_status_date]
                                ,pol.[pol_expropriation_value]
                                ,pol.[pol_immediate_refund]
                                ,pol.[pol_sale_date]
                                ,pol.[pol_transaction_date]
                                ,pol.[pol_approved_date]
                                ,pol.[pol_issue_date]
                                ,pol.[pol_account_date]
                                ,pol.[pol_agree_date]
                                ,pol.[pol_distribution]
                                ,pol.[pol_distribution_other_detail]
                                ,pol.[pol_license]
                                ,pol.[pol_re_insured_contact_number]
                                ,pol.[pol_life_detail]
           
                            ,cus.cust_type AS 'customer.cust_type'
                            ,cus.cust_type_other_detail AS 'customer.cust_type_other_detail'
                            ,cus.cust_id_type AS 'customer.cust_id_type'
                            ,cus.cust_id AS 'customer.cust_id'
                            ,cus.cust_company_name AS 'customer.cust_company_name'
                            ,cus.cust_first_name AS 'customer.cust_first_name'
                            ,cus.cust_last_name AS 'customer.cust_last_name'
                            ,cus.cust_age AS 'customer.cust_age'
                            ,cus.cust_birthday AS 'customer.cust_birthday'
                            ,cus.cust_gender AS 'customer.cust_gender'
                            ,cus.cust_address AS 'customer.cust_address'
                            ,cus.cust_sub_district AS 'customer.cust_sub_district'
                            ,cus.cust_district AS 'customer.cust_district'
                            ,cus.cust_province AS 'customer.cust_province'
                            ,cus.cust_zip_code AS 'customer.cust_zip_code'
                            ,cus.cust_country_code AS 'customer.cust_country_code'
                            ,cus.cust_phone_number AS 'customer.cust_phone_number'
                            ,cus.cust_mobile_number AS 'customer.cust_mobile_number'
                            ,cus.cust_email AS 'customer.cust_email'
                            ,cus.cust_issued_age AS 'customer.cust_issued_age'
                            ,cus.cust_race AS 'customer.cust_race'
                            ,cus.cust_nationality AS 'customer.cust_nationality'
                            ,cus.cust_religion AS 'customer.cust_religion'
                            ,cus.cust_occupation_level AS 'customer.cust_occupation_level'
                            ,cus.cust_occupation_level_other_detail AS 'customer.cust_occupation_level_other_detail'
                            ,cus.cust_occupation AS 'customer.cust_occupation'
                            ,cus.cust_job AS 'customer.cust_job'
                            ,cus.cust_position AS 'customer.cust_position'
                            ,cus.cust_business_type AS 'customer.cust_business_type'
                            ,cus.cust_income_per_year AS 'customer.cust_income_per_year'
                            ,cus.cust_us_person AS 'customer.cust_us_person'
                            ,cus.cust_us_person_born AS 'customer.cust_us_person_born'
                            ,cus.cust_us_person_green_card AS 'customer.cust_us_person_green_card'
                            ,cus.cust_us_person_vat AS 'customer.cust_us_person_vat'
                            ,cus.cust_us_person_183 AS 'customer.cust_us_person_183'
                            ,cus.cust_address_permanent AS 'customer.cust_address_permanent'
                            ,cus.cust_sub_district_permanent AS 'customer.cust_sub_district_permanent'
                            ,cus.cust_district_permanent AS 'customer.cust_district_permanent'
                            ,cus.cust_province_permanent AS 'customer.cust_province_permanent'
                            ,cus.cust_address_work AS 'customer.cust_address_work'
                            ,cus.cust_sub_district_work AS 'customer.cust_sub_district_work'
                            ,cus.cust_district_work AS 'customer.cust_district_work'
                            ,cus.cust_province_work AS 'customer.cust_province_work'
                            ,cus.cust_address_current AS 'customer.cust_address_current'
                            ,cus.cust_sub_district_current AS 'customer.cust_sub_district_current'
                            ,cus.cust_district_current AS 'customer.cust_district_current'
                            ,cus.cust_province_current AS 'customer.cust_province_current'
 
                            ,/***** insureds *****/
                                 (select ins.[insrd_cov_plan_seq]
                                    ,ins.[insrd_seq]
                                    ,ins.[insrd_type]
                                    ,ins.[insrd_id_type]
                                    ,ins.[insrd_id]
                                    ,ins.[insrd_first_name]
                                    ,ins.[insrd_last_name]
                                    ,ins.[insrd_age]
                                    ,ins.[insrd_birthday]
                                    ,ins.[insrd_gender]
                                    ,ins.[insrd_address]
                                    ,ins.[insrd_sub_district]
                                    ,ins.[insrd_district]
                                    ,ins.[insrd_province]
                                    ,ins.[insrd_zip_code]
                                    ,ins.[insrd_country_code]
                                    ,ins.[insrd_phone_number]
                                    ,ins.[insrd_mobile_number]
                                    ,ins.[insrd_email]
                                    ,ins.[insrd_issued_age]
                                    ,ins.[insrd_race]
                                    ,ins.[insrd_nationality]
                                    ,ins.[insrd_religion]
                                    ,ins.[insrd_occupation_level]
                                    ,ins.[insrd_occupation_level_other_detail]
                                    ,ins.[insrd_occupation]
                                    ,ins.[insrd_job]
                                    ,ins.[insrd_position]
                                    ,ins.[insrd_business_type]
                                    ,ins.[insrd_income_per_year]
                                    /**** insrd_pol_sub_ids *****/
                                    ,ins.[insrd_pol_sub_ids]

                                    ,/***** insrd_beneficiaries *****/
                                    (select  distinct 
                                        insbene.insrd_beneficiary_seq_no
                                        ,insbene.insrd_beneficiary_name
                                        ,insbene.insrd_beneficiary_relation
                                        ,insbene.insrd_beneficiary_relation_other_detail
                                        ,insbene.insrd_benefit_ratio
                                        FROM Temp_insuredBeneficiary as insbene 
                  
                                        WHERE ins.[insrd_beneficiaries] = insbene.[PolicyRef] 
                  
                                        FOR JSON Auto, INCLUDE_NULL_VALUES) AS insrd_beneficiaries

                                FROM Temp_insured as ins
                                WHERE pol.[pol_refer_code_of_company] COLLATE SQL_Latin1_General_CP1_CI_AS = ins.[pol_refer_code_of_company] COLLATE SQL_Latin1_General_CP1_CI_AS
                                FOR JSON Auto, INCLUDE_NULL_VALUES) AS insureds
                            ,
                            /***** sub_policies *****/
                                (select subpo.[sub_pol_id]
                                ,subpo.[sub_pol_type]
                                ,subpo.[sub_pol_ppt]
                                ,subpo.[sub_pol_start_date]
                                ,subpo.[sub_pol_end_date]
                                ,subpo.[sub_pol_sum_insured_per_year_exclude]
                                ,subpo.[sub_pol_sum_insured_per_year_include]
                                ,subpo.policy_cov_plan_seqs
                                ,/******* coverages ********/
                                (select cov.[cov_code]
                                    ,cov.[cov_detail]
                                    ,cov.[cov_amt]
                                    ,cov.[cov_sum_insured_per_coverage_per_year]
                                    ,cov.[cov_sum_insured_per_coverage_per_time]
                                    ,cov.[cov_co_payment]
                                    ,cov.[cov_deductible_amount]
                                    ,cov.[cov_deductible_detail]
                                    ,cov.[cov_number_of_day]
                                    ,cov.[cov_cover_year]
                                    ,cov.[cov_payment_year]
                                    ,cov.[cov_claim_status]
                                    FROM Temp_coverage as cov 
                                    WHERE subpo.[pol_refer_code_of_company] = cov.[pol_refer_code_of_company]
                                    FOR JSON Auto, INCLUDE_NULL_VALUES) AS coverages

                            FROM Temp_sub_policies as subpo

                            WHERE pol.[pol_refer_code_of_company] COLLATE SQL_Latin1_General_CP1_CI_AS = subpo.[pol_refer_code_of_company] COLLATE SQL_Latin1_General_CP1_CI_AS  

                            FOR JSON Auto, INCLUDE_NULL_VALUES) AS sub_policies

                    ,(select ben.beneficiary_seq_no
                            ,ben.beneficiary_name
                            ,ben.beneficiary_relation
                            ,ben.beneficiary_relation_other_detail
                            ,ben.benefit_ratio
  
                        FROM Temp_beneficiary as ben
 
                        WHERE pol.Polno = ben.Obenefit_Polno

                        FOR JSON Auto, INCLUDE_NULL_VALUES) AS beneficiaries

                    ,pol.endorsements 

                    ,pol.loans 

                    ,pol.loan_payments 

                    ,pol.investments 
        
                    FROM Temp_policies_26_2_67 pol

                    INNER JOIN Temp_Customers cus
                        ON pol.customer COLLATE SQL_Latin1_General_CP1_CI_AS = cus.cust_id COLLATE SQL_Latin1_General_CP1_CI_AS

                    -- WHERE pol.ref_seq >= 1 and pol.ref_seq <= 1000
                
                    ORDER BY pol.ref_seq ASC
                                    
                  FOR JSON PATH, INCLUDE_NULL_VALUES

                 END
                ";


        private void FormStorePolicy_Load(object sender, EventArgs e)
        {

        }

        private void button_Click_Run(object sender, EventArgs e)
        {
            PaymentStorePoc policyStorePoc = new PaymentStorePoc();
            string query = "CREATE PROCEDURE " + fileStoreProc.Text + " AS " + richTextPolicy.Text;
            string result = policyStorePoc.policystoreproc(query);
            MessageBox.Show(result);
        }

        private void ListBoxPolicy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxPolicy_Enter_1(object sender, EventArgs e)
        {

        }

        private void richTextPolicy_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fileStoreProc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
