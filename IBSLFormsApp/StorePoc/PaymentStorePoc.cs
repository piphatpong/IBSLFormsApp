using System.Data.SqlClient;

namespace IBSLFormsApp.StorePoc
{
    public class PaymentStoreProcModel
    {
        public String Result_Jsontxt;
        public string paymentstoreprocmodel(string whereConPolicy)
        {
            string result = null;
            try
            {
                //String connectionString = "Data Source=10.20.25.101;Initial Catalog=IBS_Life;User ID=devconnect;Password=P@ssw0rd1234";

                String connectionString = "Data Source=VELA\\SQLEXPRESS;Initial Catalog=IBS_Life;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string query =
                 @"
                    CREATE PROCEDURE Select_IBS_Payment

                  AS
                  BEGIN

                    SELECT pmt.[pmt_refer_code_of_company]
                              ,pmt.[pmt_transaction_status]
                              ,pmt.[pmt_company_id]
                              ,pmt.[pmt_pol_id]
                              ,pmt.[pmt_pol_refer_code_of_company]
                              ,pmt.[pmt_id]
                              ,pmt.[pmt_type]
                              ,pmt.[pmt_direct_premium]
                              ,pmt.[pmt_premium_payment_period_year]
                              ,pmt.[pmt_premium_payment_year]
                              ,pmt.[pmt_payment_period]

                          ,/***** payment_period_seqs *****/
                           (select seq.[pmt_prd_premium_seq]
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
                                   
                                   FROM [ibslife_Payment_Premium_Type_Rider] as rid
                                   WHERE rid.[pmt_premium_rid_number] = seq.[payment_premium_type_riders]
                                   FOR JSON Auto, INCLUDE_NULL_VALUES) AS payment_premium_type_riders

                                  ,/*********** payment_premium_type_endorsements **********/
                                  (select eds.[pmt_premium_edm_number]
                                        ,eds.[pmt_premium_edm_amount]
                                   
                                   FROM [ibslife_Payment_Premium_Type_Endorsement] as eds
                                   WHERE eds.[pmt_premium_edm_number] = seq.[payment_premium_type_endorsements]
                                   FOR JSON Auto, INCLUDE_NULL_VALUES) AS payment_premium_type_endorsements

                              FROM [ibslife_Payment_Period_Seq] as seq
                              WHERE pmt.[pmt_id] = seq.[pmt_id] 
                              FOR JSON Auto, INCLUDE_NULL_VALUES) AS payment_period_seqs
                                                 
                  FROM [ibslife_Payment] pmt                  
                  FOR JSON PATH, INCLUDE_NULL_VALUES
                
                 END
                ";

                //FOR JSON PATH, INCLUDE_NULL_VALUES ('2021-KL-874-409-55','2021-KL-874-367-510','2022-KL-874-367-699','2019-KL-874-252-2152','2018-KL-871-49-10459','3551100011331','3550700161121')
                SqlCommand cmd = new SqlCommand(whereConPolicy, connection);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    result = "success";
                    Result_Jsontxt = "Store Procedure Created Successfully";
                    Console.WriteLine("Store Procedure Created Successfully");
                }
                catch (SqlException e)
                {
                    result = e.Message;
                    Result_Jsontxt = "Error Generated. Details: " + e.Message;
                    Console.WriteLine("Error Generated. Details: " + e.Message);
                }
                finally
                {
                    connection.Close();
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return result;
        }
    }
}
