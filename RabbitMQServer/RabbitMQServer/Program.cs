using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQServer
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
            //        var i = 50;
            //        while (i>0)
            //        {
            //            channel.QueueDeclare("task_queue", true, false, false, null);
            //            string message = i.ToString();
            //            var body = Encoding.UTF8.GetBytes(message);

            //            var properties = channel.CreateBasicProperties();
            //            properties.Persistent = true;

            //            channel.BasicPublish("", "task_queue", properties, body);
            //            Console.WriteLine($" Had send {message}");
            //            i--;
            //        }

            //    }
            //}

            //var factory = new ConnectionFactory { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        var i = 50;
            //        while (i > 0)
            //        {
            //            channel.ExchangeDeclare("logs", "fanout");
            //            string message = i.ToString();
            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish("logs", "", null, body);
            //            Console.WriteLine($" Had send {message}");
            //            i--;
            //        }

            //    }
            //}

            //var factory = new ConnectionFactory { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        args = new[] { "error", "Run.Run.Or it will explode." };
            //        channel.ExchangeDeclare("direct_logs", "direct");
            //        var serverity = (args.Length > 0) ? args[0] : "info";
            //        string message = args.Length > 1 ? string.Join(" ", args.Skip(1).ToArray()) : "Hello Kety.";
            //        var body = Encoding.UTF8.GetBytes(message);
            //        channel.BasicPublish("direct_logs", serverity, null, body);
            //        Console.WriteLine($" Had send {serverity}:{message}");
            //    }
            //}

            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    args = new[] { "kern.critical", "A critial kernel error" };
                    channel.ExchangeDeclare("topic_logs", "topic");
                    var routingKey = (args.Length > 0) ? args[0] : "anonymous.info";
                    string message = args.Length > 1 ? string.Join(" ", args.Skip(1).ToArray()) : "Hello Kety.";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("topic_logs", routingKey, null, body);
                    Console.WriteLine($" Had send {routingKey}:{message}");
                }
            }
            Console.ReadKey();
        }
    }
}
