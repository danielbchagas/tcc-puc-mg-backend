#define REST

using System.Collections.Generic;
using ECommerce.Customers.Application.Commands;
using ECommerce.Customers.Application.Handlers.Commands;
using ECommerce.Customers.Application.Handlers.Queries;
using ECommerce.Customers.Application.Queries;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using ECommerce.Customers.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Customers.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            #region Mediator - Comandos
            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CreateCustomerCommandHandler>();
            services.AddScoped<IRequestHandler<DisableCustomerCommand, ValidationResult>, DisableCustomerCommandHandler>();

            services.AddScoped<IRequestHandler<CreateAddressCommand, ValidationResult>, CreateAddressCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAddressCommand, ValidationResult>, UpdateAddressCommandHandler>();

            services.AddScoped<IRequestHandler<CreateDocumentCommand, ValidationResult>, CreateDocumentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateDocumentCommand, ValidationResult>, UpdateDocumentCommandHandler>();

            services.AddScoped<IRequestHandler<CreateEmailCommand, ValidationResult>, CreateEmailCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEmailCommand, ValidationResult>, UpdateEmailCommandHandler>();

            services.AddScoped<IRequestHandler<CreatePhoneCommand, ValidationResult>, CreatePhoneCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePhoneCommand, ValidationResult>, UpdatePhoneCommandHandler>();
            #endregion

            #region Mediator - Queries
            services.AddScoped<IRequestHandler<GetCustomerQuery, Domain.Models.Customer>, GetCustomerQueryHandler>();
            
            services.AddScoped<IRequestHandler<GetAddressQuery, Address>, GetAddressQueryHandler>();

            services.AddScoped<IRequestHandler<GetEmailQuery, Email>, GetEmailQueryHandler>();

            services.AddScoped<IRequestHandler<GetDocumentQuery, Document>, GetDocumentQueryHandler>();

            services.AddScoped<IRequestHandler<GetPhoneQuery, Phone>, GetPhoneQueryHandler>();
            #endregion

            #region Repositórios
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();

            #endregion

#if RABBITMQ
            services.AddHostedService<CreateCustomerIntegrationHandler>();
            services.AddHostedService<CreateDocumentIntegrationHandler>();
            services.AddHostedService<CreateEmailIntegrationHandler>();
            services.AddHostedService<CreatePhoneIntegrationHandler>();
#endif
        }
    }
}
