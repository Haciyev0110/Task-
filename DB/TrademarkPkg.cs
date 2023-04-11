using Container.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public static partial class MSDB
    {

        public static async Task<List<Trademark>> GetTrademarksAsync(string word)
        {
            SqlConnection connection = null;

            List<Trademark> response = new List<Trademark>();

            try
            {
                connection = GetConnection();

                using (SqlCommand command =new SqlCommand("select * from SearchTrademarkByWord(@SearchWord)", connection) )
                {
                    command.CommandType = System.Data.CommandType.Text;

                    command.Parameters.AddWithValue("@SearchWord",word);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {

                        while (reader.Read())
                        {
                            response.Add(new Trademark() 
                            {
                            
                              Number = reader["TrademarkID"].GetInt(),
                               Classes = reader["TrademarkClasses"].GetInt(),
                               Url = reader["TrademarkLogoUrl"].GetString(),
                               Status1 = reader["TrademarkStatus1"].GetString(),
                               Status2 = reader["TrademarkStatus2"].GetString(),
                               Details = reader["TrademarkDetailsPage"].GetString(),
                               Name = reader["TrademarkName"].GetString()
                            });
                        }
                    }

                }
            }
            catch (Exception exp)
            {
                Log.Error(exp, exp.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return response;
        }
    }
}
