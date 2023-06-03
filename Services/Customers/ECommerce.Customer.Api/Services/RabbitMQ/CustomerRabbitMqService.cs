using ECommerce.Customer.Api.Models;
using ECommerce.Customers.Application.Commands.User;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customer.Api.Services.RabbitMQ
{
    public class CustomerRabbitMqService : BackgroundService
    {
        private readonly RabbitMqOption _rabbitMQOptions;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public CustomerRabbitMqService(IOptions<RabbitMqOption> rabbitMQOptions, IServiceProvider serviceProvider)
        {
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
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitMQOptions.Queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

            await Task.CompletedTask;
        }

        private async Task<ValidationResult> AddCustomer(CreateCustomerCommand request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                ValidationResult result = await mediator.Send(request);

                return await Task.FromResult(result);
            }
        }
    }
}
