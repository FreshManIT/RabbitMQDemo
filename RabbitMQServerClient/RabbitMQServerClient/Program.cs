using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var factory = new ConnectionFactory { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        while (true)
            //        {
            //            channel.QueueDeclare("task_queue", true, false, false, null);

            //            channel.BasicQos(0, 1, false);
            //            Console.WriteLine("Waiting for message.");
            //            var consumer = new EventingBasicConsumer(channel);
            //            consumer.Received += (model, ea) =>
            //            {
            //                var body = ea.Body;
            //                var message = Encoding.UTF8.GetString(body);
            //                Console.WriteLine($"Received {message}");
            //                channel.BasicAck(ea.DeliveryTag, false);
            //            };
            //            channel.BasicConsume("task_queue", false, consumer);

            //            Thread.Sleep(3000);
            //        }
            //    }
            //}

            //var factory = new ConnectionFactory { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        while (true)
            //        {
            //            channel.ExchangeDeclare("logs", "fanout");

            //            var queueName = channel.QueueDeclare().QueueName;
            //            channel.QueueBind(queueName, "logs", "");
            //            Console.WriteLine("Waiting for logs.");
            //            var consumer = new EventingBasicConsumer(channel);
            //            consumer.Received += (model, ea) =>
            //            {
            //                var body = ea.Body;
            //                var message = Encoding.UTF8.GetString(body);
            //                Console.WriteLine($"Received {message}");
            //            };
            //            channel.BasicConsume(queueName, true, consumer);
            //            Thread.Sleep(3000);
            //        }
            //    }
            //}

            //var factory = new ConnectionFactory { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        //while (true)
            //        //{
            //            channel.ExchangeDeclare("direct_logs", "direct");

            //            var queueName = channel.QueueDeclare().QueueName;
            //            args = new[] { "warning", "error" };
            //            if (args.Length < 1)
            //            {
            //                Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
            //                      Environment.GetCommandLineArgs()[0]);
            //                Console.WriteLine(" Press [enter] to exit.");
            //                Console.ReadLine();
            //                Environment.ExitCode = 1;
            //                return;
            //            }
            //            foreach (var severity in args)
            //            {
            //                channel.QueueBind(queueName, "direct_logs", severity);
            //            }
            //            Console.WriteLine("Waiting for logs.");
            //            var consumer = new EventingBasicConsumer(channel);
            //            consumer.Received += (model, ea) =>
            //            {
            //                var body = ea.Body;
            //                var message = Encoding.UTF8.GetString(body);
            //                var routingKey = ea.RoutingKey;
            //                Console.WriteLine($"Received {routingKey}:{message}");
            //            };
            //            channel.BasicConsume(queueName, true, consumer);
            //            Thread.Sleep(3000);
            //        //}
            //    }
            //}

            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    while (true)
                    {
                        channel.ExchangeDeclare("topic_logs", "topic");

                    var queueName = channel.QueueDeclare().QueueName;
                    //args = new[] { "#" };
                    args = new[] { "kern.*" };
                    //args = new[] { "*.critical" };
                    //args = new[] { "kern.*", "*.critical" };
                    if (args.Length < 1)
                    {
                        Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                              Environment.GetCommandLineArgs()[0]);
                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                        Environment.ExitCode = 1;
                        return;
                    }
                    foreach (var severity in args)
                    {
                        channel.QueueBind(queueName, "topic_logs", severity);
                    }
                    Console.WriteLine("Waiting for logs.");
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;
                        Console.WriteLine($"Received {routingKey}:{message}");
                    };
                    channel.BasicConsume(queueName, true, consumer);
                    Thread.Sleep(3000);
                    }
                }
            }
        }
    }
}
