using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageReceiver
{
    public class MQReceiver
    {
        private readonly ProductRepository _productRepository;
        public MQReceiver(string connectionString)
        {
            _productRepository = new ProductRepository(connectionString);
        }

        public void Receive()
        {
            string queueName = "csv_product";
            var factory = new ConnectionFactory() { HostName = "localhost", Port=5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    var product = Product.FromCSV(message);

                    int re = _productRepository.InsertProduct(product);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
