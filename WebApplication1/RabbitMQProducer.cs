using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Rab
{
    public class RabbitMQProducer
    {
       public async Task<bool> Send(string routingKey, Tour tour)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var message = "hello world";

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("orders", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);

            return true;
        }

    }
}
