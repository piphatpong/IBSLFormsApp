using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace IBSLFormsApp.StorePoc
{
    public class PaymentStorePoc 
    { 
        public string policystoreproc(string whereConPolicy)
        {
            string result = null;

            try
            {
                //String connectionString = "Data Source=10.20.25.101;Initial Catalog=IBS_Life;User ID=devconnect;Password=P@ssw0rd1234";

                String connectionString = "Data Source=VELA\\SQLEXPRESS;Initial Catalog=IBS_Life;Integrated Security=True";
                
                SqlConnection connection = new SqlConnection(connectionString);
                
                //FOR JSON PATH, INCLUDE_NULL_VALUES ('2021-KL-874-409-55','2021-KL-874-367-510','2022-KL-874-367-699','2019-KL-874-252-2152','2018-KL-871-49-10459','3551100011331','3550700161121')
                SqlCommand cmd = new SqlCommand(whereConPolicy, connection);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    result = "success";
                    //Console.WriteLine("Store Procedure Created Successfully");
                }
                catch (SqlException e)
                {
                    result = e.Message;
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
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
