using ECommerce.Identity.Api.Descriptors.Request;
using RabbitMQ.Client;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Services
{
    public class CreateCustomerIntegrationService
    {
        private string Host { get; }
        private string Username { get; }
        private string Password { get; }
        private string Queue { get; }
        private string Exchange { get; set; }

        public CreateCustomerIntegrationService(string host, string username, string password, string queue, string exchange)
        {
            Host = host;
            Username = username;
            Password = password;
            Queue = queue;
            Exchange = exchange;
        }

        public Task CreateCustomer(CustomerRequest request)
        {
            var factory = new ConnectionFactory 
            { 
                HostName = Host, 
                UserName = Username, 
                Password = Password 
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = JsonSerializer.SerializeToUtf8Bytes(request);

            var properties = channel.CreateBasicProperties();
            properties.Persistent= true;

            channel.BasicPublish(exchange: Exchange,
                                 routingKey: Queue,
                                 basicProperties: properties,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}
