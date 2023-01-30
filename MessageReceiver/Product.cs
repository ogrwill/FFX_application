using System;
using System.Collections.Generic;
using System.Text;

namespace MessageReceiver
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public static Product FromCSV(string csvLine)
        {
            try
            {
                string[] values = csvLine.Split(',');
                Product product = new Product()
                {
                    ID = Convert.ToInt32(values[0]),
                    Title = values[1],
                    Price = Convert.ToDecimal(values[2])
                };
                return product;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Converting Product:{csvLine}: {ex.Message}");
                return null;
            }
        }
    }
}
