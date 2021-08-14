using EasyNetQ;
using ECommerce.Cliente.Api.Models;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.WebApi.Core.DTOs;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Api.Services
{
    class AdicionarClienteIntegrationHandler : BackgroundService
    {
        private readonly RabbitMQOptions _rabbitMQOptions;
        private readonly IServiceProvider _serviceProvider;
        
        public AdicionarClienteIntegrationHandler(IOptions<RabbitMQOptions> rabbitMQOptions, IServiceProvider serviceProvider)
        {
            _rabbitMQOptions = rabbitMQOptions.Value;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bus = RabbitHutch.CreateBus($"host={_rabbitMQOptions.Endereco}:{_rabbitMQOptions.Porta}");
            bus.Rpc.RespondAsync<ClienteDTO, ValidationResult>(async request => await AdicionarCliente(request), stoppingToken);
            
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

            ValidationResult valido;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                valido = await mediator.Send(clienteCommand);
            }

            return await Task.FromResult(valido);
        }
    }
}
