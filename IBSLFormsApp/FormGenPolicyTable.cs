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
    public partial class FormGenPolicyTable : Form
    {
        public FormGenPolicyTable()
        {
            InitializeComponent();
        }

        public string GenPolycy_query_stmt = @"BEGIN
            SELECT
            (ROW_NUMBER() OVER (ORDER BY polno)) AS ref_seq
            ,CONCAT(YEAR(ols.approvedate),'-OR-',ols.poltype,'-',ols.Polno,'-',ols.polid) AS pol_refer_code_of_company
            ,'N' AS pol_transaction_status
            ,'1013' AS pol_company_id
            ,Polno AS pol_id
            ,(SELECT sahpro.ibs_poltype FROM saha_product_type as sahpro WHERE sahpro.poltype = ols.poltype) AS pol_type 
            ,null AS pol_type_gli_name
            ,null AS pol_type_gli_plan

            ,(SELECT sahpro.ibs_producttype FROM saha_product_type as sahpro WHERE sahpro.poltype = ols.poltype) AS pol_product_type

            ,(SELECT iSer.ins_name FROM ibsl_iserf_test iSer 
                WHERE iSer.poltype = ols.poltype and ols.poltypesub = iSer.poltype_sub_min and ((ols.age <= iSer.flag2 and ols.age >= iSer.flag1) or iSer.flag1 = '999')) AS pol_product_type_other_detail

            ,(SELECT sahpro.pol_kind FROM saha_product_type as sahpro WHERE sahpro.poltype = ols.poltype) AS pol_type_2

            ,(SELECT iser.iSerffCode FROM ibsl_iserf_test iser WHERE (ols.poltype <> '29' and iser.poltype = ols.poltype and iser.poltype_sub_min = ols.poltypesub) 
            or (ols.poltype = '29' and  ols.age >= iser.flag1 and ols.age <= iser.flag2 and iser.poltype = ols.poltype and iser.poltype_sub_min = ols.poltypesub) ) AS pol_sub_type

            ,(SELECT opo.Polname FROM Opoltype opo WHERE ols.poltype = opo.Poltype) AS pol_name

            ,(SELECT iSer.ins_name FROM ibsl_iserf_test iSer 
                WHERE iSer.poltype = ols.poltype and ols.poltypesub = iSer.poltype_sub_min and ((ols.age <= iSer.flag2 and ols.age >= iSer.flag1) or iSer.flag1 = '999') )
                AS pol_name_oic

            ,Ols.startdate AS pol_start_date
            ,Ols.finishdate AS pol_end_date

            ,null AS pol_claim_document
            ,null AS pol_payment_term
            ,null AS pol_payment_amount
            ,null AS pol_payment_period
            ,null AS pol_info_url
            ,null AS pol_claim_url
            ,null AS pol_document_urls
            ,null AS pol_year
            -- ,(CASE WHEN ols.polstatus = '10' THEN '0010' ELSE CASE WHEN ols.polstatus = '30' THEN '0500' ELSE CASE WHEN ols.polstatus = '31' THEN '0600' END END END) AS pol_status
            ,(SELECT CASE WHEN pol.PolStatusId is not null THEN pol.PolStatusId ELSE '9999' END From cpolstatus cpo INNER JOIN ibsl_PolicyStatus pol ON UPPER(cpo.StaPolName) = UPPER(pol.PolStatusENDes) WHERE cpo.StaPol = ols.polstatus) AS pol_status
            ,null AS pol_status_note
            ,ols.statusmemodate AS pol_status_date
            ,null AS pol_expropriation_value
            ,null AS pol_immediate_refund
            ,null AS pol_sale_date
            ,null AS pol_transaction_date
            ,Ols.approvedate AS pol_approved_date
            ,null AS pol_issue_date
            ,null AS pol_account_date
            ,null AS pol_agree_date
            ,null AS pol_distribution
            ,null AS pol_distribution_other_detail
            ,null AS pol_license
            ,null AS pol_re_insured_contact_number
            ,null AS pol_life_detail
            ,ols.personalid AS customer
            ,ols.personalid AS insureds

            , (select CONCAT(iser.iSerffCode,'-',ols.Polno) AS sub_pol_id
	            from ibsl_iserf_test iser
	            Where (iser.poltype = ols.poltype and iser.poltype_sub_min = ols.poltypesub) 
	            and 
	            ((ols.poltype='29' or ols.poltype='31') and (ols.age >= iser.flag1 and ols.age <= flag2)
	            or
	            (ols.poltype not in ('29','31'))
	            )
                ) AS sub_policies

            ,null AS beneficiaries
            ,null AS endorsements
            ,null AS loans
            ,null AS loan_payments
            ,null AS investments
            ,cast('' as character varying(2)) AS send_status
            ,cast('' as character varying(200)) AS send_res
            ,ols.PolicyKind

            -- INTO ibsl_Policies

            From Temp_Policy_IBSL ols
            Where YEAR(ols.approvedate) = '2023' and ols.approvestatus = '2' and ols.PolicyKind = 'สินเชื่อ'
            ORDER BY ref_seq ASC
        END
                                    ";

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormGenPolicyTable_Load(object sender, EventArgs e)
        {

        }
    }
}
