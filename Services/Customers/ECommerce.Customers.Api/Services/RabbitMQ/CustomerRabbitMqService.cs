using ECommerce.Customer.Api.Constants;
using ECommerce.Customer.Api.Models;
using ECommerce.Customers.Application.Commands.Customer;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customer.Api.Services.RabbitMQ
{
    public class CustomerRabbitMqService : IHostedService, IDisposable
    {
        private readonly ILogger<CustomerRabbitMqService> _logger;
        private readonly IServiceProvider _serviceProvider;

        private readonly RabbitMqOption _rabbitMQOptions;
        
        private IConnection _connection;
        private IModel _channel;
        
        public CustomerRabbitMqService(ILogger<CustomerRabbitMqService> logger, IOptions<RabbitMqOption> rabbitMQOptions, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQOptions = rabbitMQOptions.Value;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQOptions.Host,
                UserName = _rabbitMQOptions.Username,
                Password = _rabbitMQOptions.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = JsonSerializer.Deserialize<CreateCustomerCommand>(body);

            _ = AddCustomer(message);
        }

        private async Task AddCustomer(CreateCustomerCommand request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var result = await mediator.Send(request);

                if (!result.Item1.IsValid)
                    _logger.LogError(CustomerMessages.CREATE_CUSTOMER_ERROR, result.Item2);
                else
                    _logger.LogInformation(CustomerMessages.CREATE_CUSTOMER_SUCCESS, result.Item2);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted Service is starting...");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume(queue: _rabbitMQOptions.Queue, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted Service is stopping...");

            _channel.Close();
            _connection.Close();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
