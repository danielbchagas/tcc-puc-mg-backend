using EasyNetQ;
using ECommerce.Cliente.Api.Models;
using ECommerce.Cliente.Application.Commands;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Api.Services
{
    public class AdicionarEmailIntegrationHandler : BackgroundService
    {
        private readonly RabbitMqOptions _rabbitMQOptions;
        private readonly IServiceProvider _serviceProvider;
        private IBus _bus;

        public AdicionarEmailIntegrationHandler(IOptions<RabbitMqOptions> rabbitMQOptions, IServiceProvider serviceProvider)
        {
            _rabbitMQOptions = rabbitMQOptions.Value;
            _serviceProvider = serviceProvider;
            _bus = RabbitHutch.CreateBus(_rabbitMQOptions.MessageBus);
            _bus.Advanced.Disconnected += OnDisconnect;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TryConnect();

            await _bus.Rpc.RespondAsync<AdicionarEmailCommand, ValidationResult>(async request => await AdicionarEmail(request), stoppingToken);

            await Task.CompletedTask;
        }

        private async Task<ValidationResult> AdicionarEmail(AdicionarEmailCommand request)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                ValidationResult valido = await mediator.Send(request);

                return await Task.FromResult(valido);
            }
        }

        private void TryConnect()
        {
            if (_bus.Advanced.IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, sleepDurationProvider: sleep => TimeSpan.FromSeconds(Math.Pow(2, sleep)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_rabbitMQOptions.MessageBus);
            });
        }

        private void OnDisconnect(object source, EventArgs e)
        {
            Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();
        }
    }
}
