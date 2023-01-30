using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageSender
{
    public class MQSender
    {
        private ConnectionFactory _factory;
        public MQSender(string host)
        {
            _factory = new ConnectionFactory() { HostName = host, Port=5672 };            
        }

        public async Task SendLines(List<string> lines, string queueName)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                      durable: false,
                                      exclusive:false,
                                      autoDelete: false,
                                      arguments:null);

                var props = channel.CreateBasicProperties();
                props.ReplyTo = queueName;

                foreach (var line in lines)
                {
                    var body = Encoding.UTF8.GetBytes(line);
                    channel.BasicPublish(exchange:"",
                        routingKey:queueName, 
                        basicProperties:props, body:body);

                    await WaitForResponse(channel, queueName);
                }

            }

          
        }
        public async Task WaitForResponse(RabbitMQ.Client.IModel channel, string queueName)
        {
            var respConsumer = new AsyncEventingBasicConsumer(channel);
            respConsumer.Received += async (model, ea) =>
            {
                var response = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"Response from Queue: {response}");
            };

            var consumer = channel.BasicConsume(queue: queueName,
                                        autoAck:true,
                                        consumer:respConsumer);
            await Task.Delay(1000);
            channel.BasicCancel(consumer);

        }
    }
}
