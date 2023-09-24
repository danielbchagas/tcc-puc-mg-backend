using System;
using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models.Request;
using ECommerce.Identity.Api.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;

namespace ECommerce.Identity.Api.Services.RabbitMQ
{
    public class CustomerRabbitMqService : ICustomerRabbitMqClient
    {
        private readonly ILogger<CustomerRabbitMqService> _logger;
        private readonly RabbitMqOption _rabbitMqOptions;

        public CustomerRabbitMqService(ILogger<CustomerRabbitMqService> logger, IOptions<RabbitMqOption> options)
        {
            _logger = logger;
            _rabbitMqOptions = options.Value;
        }

        public Task CreateCustomer(CustomerRequest request)
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .RetryAsync(3, (exception, retryCount, context) =>
                {
                    _logger.LogError($"Retry counter: {retryCount}", JsonSerializer.Serialize(exception));
                });
            
            return retryPolicy.ExecuteAsync(() =>
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

                return Task.FromResult(Task.CompletedTask);
            });
        }
    }
}
