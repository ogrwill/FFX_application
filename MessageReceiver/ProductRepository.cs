using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MessageReceiver
{
    public class ProductRepository 
    {
        private readonly string _connection;

        public ProductRepository(string sqlConnection)
        {
            _connection = sqlConnection;
        }

        public int InsertProduct(Product product)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO DB.dbo.Product VALUES (@id, '@title', @price); ", connection))
                    {
                        command.Parameters.AddWithValue("@id", product.ID);
                        command.Parameters.AddWithValue("@title", product.Title);
                        command.Parameters.AddWithValue("@price", product.Price.ToString());

                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex: InsertProduct: " + ex.Message);
                return -1;
            }
        }

    }
}
