using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost", Port= 5672 };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

// declare a server-named queue
var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName,
                  exchange: "topic_logs",
                  routingKey: "tour.Booked");

Console.WriteLine(" [*] Waiting for logs.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] {message}");
};
channel.BasicConsume(queue: queueName,
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();