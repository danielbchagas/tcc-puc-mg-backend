using ECommerce.Cliente.Api.Services;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Handlers.Commands;
using ECommerce.Cliente.Domain.Application.Handlers.Notifications;
using ECommerce.Cliente.Domain.Application.Handlers.Queries;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using ECommerce.Cliente.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ECommerce.Cliente.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            #region Mediator - Comandos
            services.AddScoped<IRequestHandler<AtualizarClienteCommand, ValidationResult>, AtualizarClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarClienteCommand, ValidationResult>, AdicionarClienteCommandHandler>();
            services.AddScoped<IRequestHandler<DesativarClienteCommand, ValidationResult>, DesativarClienteCommandHandler>();

            services.AddScoped<IRequestHandler<AtualizarEnderecoCommand, ValidationResult>, AtualizarEnderecoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarEnderecoCommand, ValidationResult>, AdicionarEnderecoCommandHandler>();

            services.AddScoped<IRequestHandler<AdicionarDocumentoCommand, ValidationResult>, AdicionarDocumentoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarDocumentoCommand, ValidationResult>, AtualizarDocumentoCommandHandler>();

            services.AddScoped<IRequestHandler<AdicionarEmailCommand, ValidationResult>, AdicionarEmailCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarEmailCommand, ValidationResult>, AtualizarEmailCommandHandler>();

            services.AddScoped<IRequestHandler<AdicionarTelefoneCommand, ValidationResult>, AdicionarTelefoneCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarTelefoneCommand, ValidationResult>, AtualizarTelefoneCommandHandler>();
            #endregion

            #region Mediator - Queries
            services.AddScoped<IRequestHandler<BuscarClientePorIdQuery, Domain.Models.Cliente>, BuscarClientesPorIdQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarClientesFiltradosPaginadosQuery, IEnumerable<Domain.Models.Cliente>>, BuscarClienteFiltradosPaginadosQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarClientesPaginadosQuery, IEnumerable<Domain.Models.Cliente>>, BuscarClientesPaginadosQueryHandler>();

            services.AddScoped<IRequestHandler<BuscarEnderecoPorIdQuery, Endereco>, BuscarEnderecoPorIdQueryHandler>();

            services.AddScoped<IRequestHandler<BuscarEmailPorIdQuery, Email>, BuscarEmailPorIdQueryHandler>();

            services.AddScoped<IRequestHandler<BuscarDocumentoPorIdQuery, Documento>, BuscarDocumentoPorIdQueryHandler>();

            services.AddScoped<IRequestHandler<BuscarTelefonePorIdQuery, Telefone>, BuscarTelefonePorIdQueryHandler>();
            #endregion

            #region Mediator - Notificações
            services.AddScoped<INotificationHandler<ClienteCommitNotification>, ClienteCommitNotificationHandler>();

            services.AddScoped<INotificationHandler<EnderecoCommitNotification>, EnderecoCommitNotificationHandler>();

            services.AddScoped<INotificationHandler<DocumentoCommitNotification>, DocumentoCommitNotificationHandler>();

            services.AddScoped<INotificationHandler<EmailCommitNotification>, EmailCommitNotificationHandler>();

            services.AddScoped<INotificationHandler<TelefoneCommitNotification>, TelefoneCommitNotificationHandler>();
            #endregion

            #region Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<ITelefoneRepository, TelefoneRepository>();

            #endregion

            services.AddHostedService<AdicionarClienteIntegrationHandler>();
        }
    }
}
