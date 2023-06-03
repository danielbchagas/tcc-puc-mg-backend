using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models.Request;
using ECommerce.Identity.Api.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Services.RabbitMQ
{
    public class CustomerRabbitMqService : ICustomerRabbitMqClient
    {
        public RabbitMqOption _rabbitMqOptions;

        public CustomerRabbitMqService(IOptions<RabbitMqOption> options)
        {
            _rabbitMqOptions = options.Value;
        }

        public Task CreateCustomer(CustomerRequest request)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.Host,
                UserName = _rabbitMqOptions.Username,
                Password = _rabbitMqOptions.Password
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _rabbitMqOptions.Queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = JsonSerializer.SerializeToUtf8Bytes(request);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: _rabbitMqOptions.Exchange,
                                 routingKey: _rabbitMqOptions.Queue,
                                 basicProperties: properties,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}
