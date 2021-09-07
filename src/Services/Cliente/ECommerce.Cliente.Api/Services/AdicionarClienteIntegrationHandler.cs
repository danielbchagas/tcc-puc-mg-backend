using EasyNetQ;
using ECommerce.Cliente.Api.Models;
using ECommerce.Cliente.Domain.Application.Commands;
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
    class AdicionarClienteIntegrationHandler : BackgroundService
    {
        private readonly RabbitMqOptions _rabbitMQOptions;
        private readonly IServiceProvider _serviceProvider;
        private IBus _bus;
        
        public AdicionarClienteIntegrationHandler(IOptions<RabbitMqOptions> rabbitMQOptions, IServiceProvider serviceProvider)
        {
            _rabbitMQOptions = rabbitMQOptions.Value;
            _serviceProvider = serviceProvider;
            _bus = RabbitHutch.CreateBus(_rabbitMQOptions.MessageBus);
            _bus.Advanced.Disconnected += OnDisconnect;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TryConnect();
            await _bus.Rpc.RespondAsync<ClienteDto, ValidationResult>(async request => await AdicionarCliente(request), stoppingToken);
            
            await Task.CompletedTask;
        }

        private async Task<ValidationResult> AdicionarCliente(ClienteDto request)
        {
            var clienteCommand = new AdicionarClienteCommand(
                id: request.Id, 
                nome: request.Nome, 
                sobrenome: request.Sobrenome, 
                documento: request.Documento, 
                telefone: request.Telefone,
                email: request.Email
            );

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                ValidationResult valido = await mediator.Send(clienteCommand);

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
