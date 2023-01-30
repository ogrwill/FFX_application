using System;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = MainAsync(args);
            t.Wait();
        }
        static async Task MainAsync(string[] args)
        {
            string queueName = "csv_product";
            string hostName = "localhost";

            MQSender mQSender = new MQSender(hostName);

            Console.WriteLine("Welcome to CSV Upload.");
            Console.WriteLine("Enter the file path to the CSV you wish to upload:");
            var filePath = Console.ReadLine();
            if (!String.IsNullOrEmpty(filePath))
            {
                var response = CSVHelper.UploadCSV(filePath);

                if (response.Error)
                {
                    Console.WriteLine(response.Message);
                }
                else
                {                  
                    //send to queue
                    await mQSender.SendLines(response.Lines, queueName);
                }
            }
            else
            {
                Console.WriteLine("No file path entered");
            }
            Console.ReadLine();
        }
    }
}
