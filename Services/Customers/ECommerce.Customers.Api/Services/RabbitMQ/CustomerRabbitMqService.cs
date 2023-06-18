using ECommerce.Customer.Api.Models;
using ECommerce.Customers.Application.Commands.Customer;
using FluentValidation.Results;
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
        private readonly RabbitMqOption _rabbitMQOptions;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;
        private Timer _timer = null;

        public CustomerRabbitMqService(ILogger<CustomerRabbitMqService> logger, IOptions<RabbitMqOption> rabbitMQOptions, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQOptions = rabbitMQOptions.Value;
            _serviceProvider = serviceProvider;
            ConfigureChannel();
        }

        private void ConfigureChannel()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQOptions.Host,
                UserName = _rabbitMQOptions.Username,
                Password = _rabbitMQOptions.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitMQOptions.Queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        private void DoWork(object state)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<CreateCustomerCommand>(body);

                await AddCustomer(message);

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: _rabbitMQOptions.Queue,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private async Task<(ValidationResult, Customers.Domain.Models.Customer)> AddCustomer(CreateCustomerCommand request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var result = await mediator.Send(request);

                return await Task.FromResult((result.Item1, result.Item2));
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted Service running...");

#if DEBUG
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                               TimeSpan.FromMinutes(1));
#else
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(5));
#endif

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping...");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
