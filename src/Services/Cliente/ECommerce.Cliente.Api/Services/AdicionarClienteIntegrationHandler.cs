using EasyNetQ;
using ECommerce.Cliente.Api.Models;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.WebApi.Core.DTOs;
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
        private readonly RabbitMQOptions _rabbitMQOptions;
        private readonly IServiceProvider _serviceProvider;
        private IBus _bus;
        
        public AdicionarClienteIntegrationHandler(IOptions<RabbitMQOptions> rabbitMQOptions, IServiceProvider serviceProvider)
        {
            _rabbitMQOptions = rabbitMQOptions.Value;
            _serviceProvider = serviceProvider;
            _bus = RabbitHutch.CreateBus($"host={_rabbitMQOptions.Endereco}:{_rabbitMQOptions.Porta}");
            _bus.Advanced.Disconnected += OnDisconnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TryConnect();
            _bus.Rpc.RespondAsync<ClienteDTO, ValidationResult>(async request => await AdicionarCliente(request), stoppingToken);
            
            return Task.CompletedTask;
        }

        private async Task<ValidationResult> AdicionarCliente(ClienteDTO request)
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
                _bus = RabbitHutch.CreateBus($"host={_rabbitMQOptions.Endereco}:{_rabbitMQOptions.Porta}");
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
