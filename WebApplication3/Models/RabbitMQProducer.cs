using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Rab
{
    public class RabbitMQProducer
    {

       public async Task<bool> Send(string routingKey, Tour tour)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost",
                                                     Port=5672};
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

               
                var body = Encoding.UTF8.GetBytes(tour.ToString());
                channel.BasicPublish(exchange: "topic_logs",
                         routingKey: routingKey,
                         basicProperties: null,
                         body: body);

            }
            catch (Exception ex)
            {
                var t = ex;
                return false;
            }

            return true;
        }

    }
}
