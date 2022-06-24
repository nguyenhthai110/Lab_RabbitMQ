using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Subscribe.Entities;
using System;
using System.Text;

namespace Subscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.1.49", Port = 4141, UserName = "admin", Password = "admin" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MessageDataEntity", durable: true, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "MessageDataEntity", autoAck: true, consumer: consumer);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    if (body != null && body.Length > 0)
                    {
                        var message = Encoding.UTF8.GetString(body);
                        MessageDataEntity entity = JsonConvert.DeserializeObject<MessageDataEntity>(message);
                        Console.WriteLine(" [x] Received {0}", entity);
                    }

                };

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
