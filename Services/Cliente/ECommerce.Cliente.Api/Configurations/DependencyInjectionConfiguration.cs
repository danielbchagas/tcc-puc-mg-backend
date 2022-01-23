#define REST

using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Application.Handlers.Commands;
using ECommerce.Cliente.Application.Handlers.Queries;
using ECommerce.Cliente.Application.Queries;
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
            services.AddScoped<IRequestHandler<CreateClienteCommand, ValidationResult>, CreateClienteCommandHandler>();
            services.AddScoped<IRequestHandler<DisableClienteCommand, ValidationResult>, DisableClienteCommandHandler>();

            services.AddScoped<IRequestHandler<CreateEnderecoCommand, ValidationResult>, CreateEnderecoCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEnderecoCommand, ValidationResult>, UpdateEnderecoCommandHandler>();

            services.AddScoped<IRequestHandler<CreateDocumentoCommand, ValidationResult>, CreateDocumentoCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateDocumentoCommand, ValidationResult>, UpdateDocumentoCommandHandler>();

            services.AddScoped<IRequestHandler<CreateEmailCommand, ValidationResult>, CreateEmailCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEmailCommand, ValidationResult>, UpdateEmailCommandHandler>();

            services.AddScoped<IRequestHandler<CreateTelefoneCommand, ValidationResult>, CreateTelefoneCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTelefoneCommand, ValidationResult>, UpdateTelefoneCommandHandler>();
            #endregion

            #region Mediator - Queries
            services.AddScoped<IRequestHandler<GetClienteQuery, Domain.Models.Cliente>, GetClienteQueryHandler>();
            services.AddScoped<IRequestHandler<FilterClientesQuery, IEnumerable<Domain.Models.Cliente>>, FilterClientesQueryHandler>();
            services.AddScoped<IRequestHandler<GetClientesQuery, IEnumerable<Domain.Models.Cliente>>, GetClientesQueryHandler>();

            services.AddScoped<IRequestHandler<GetEnderecoQuery, Endereco>, GetEnderecoQueryHandler>();

            services.AddScoped<IRequestHandler<GetEmailQuery, Email>, GetEmailQueryHandler>();

            services.AddScoped<IRequestHandler<GetDocumentoQuery, Documento>, GetDocumentoQueryHandler>();

            services.AddScoped<IRequestHandler<GetTelefoneQuery, Telefone>, GetTelefoneQueryHandler>();
            #endregion

            #region Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<ITelefoneRepository, TelefoneRepository>();

            #endregion

#if RABBITMQ
            services.AddHostedService<AdicionarClienteIntegrationHandler>();
            services.AddHostedService<AdicionarDocumentoIntegrationHandler>();
            services.AddHostedService<AdicionarEmailIntegrationHandler>();
            services.AddHostedService<AdicionarTelefoneIntegrationHandler>();
#endif
        }
    }
}
