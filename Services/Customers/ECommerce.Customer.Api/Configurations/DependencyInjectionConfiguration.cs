#define RABBITMQ

using ECommerce.Customer.Api;
using ECommerce.Customer.Api.Services.RabbitMQ;
using ECommerce.Customers.Application.Commands.Address;
using ECommerce.Customers.Application.Commands.Document;
using ECommerce.Customers.Application.Commands.Email;
using ECommerce.Customers.Application.Commands.Phone;
using ECommerce.Customers.Application.Commands.User;
using ECommerce.Customers.Application.Handlers.Commands.Address;
using ECommerce.Customers.Application.Handlers.Commands.Document;
using ECommerce.Customers.Application.Handlers.Commands.Email;
using ECommerce.Customers.Application.Handlers.Commands.Phone;
using ECommerce.Customers.Application.Handlers.Commands.User;
using ECommerce.Customers.Application.Handlers.Queries;
using ECommerce.Customers.Application.Queries;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using ECommerce.Customers.Infrastructure.Data;
using ECommerce.Customers.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ECommerce.Customer.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            #region
            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CreateCustomerCommandHandler>();
            services.AddScoped<IRequestHandler<DisableCustomerCommand, ValidationResult>, DisableCustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, ValidationResult>, UpdateCustomerCommandHandler>();

            services.AddScoped<IRequestHandler<CreateAddressCommand, ValidationResult>, CreateAddressCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAddressCommand, ValidationResult>, UpdateAddressCommandHandler>();

            services.AddScoped<IRequestHandler<UpdateDocumentCommand, ValidationResult>, UpdateDocumentCommandHandler>();

            services.AddScoped<IRequestHandler<UpdateEmailCommand, ValidationResult>, UpdateEmailCommandHandler>();

            services.AddScoped<IRequestHandler<UpdatePhoneCommand, ValidationResult>, UpdatePhoneCommandHandler>();
            #endregion

            #region
            services.AddScoped<IRequestHandler<GetUserQuery, Customers.Domain.Models.Customer>, GetCustomerQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllUsersQuery, IList<Customers.Domain.Models.Customer>>, GetAllCustomerQueryHandler>();

            services.AddScoped<IRequestHandler<GetAddressQuery, Address>, GetAddressQueryHandler>();

            services.AddScoped<IRequestHandler<GetEmailQuery, Email>, GetEmailQueryHandler>();

            services.AddScoped<IRequestHandler<GetDocumentQuery, Document>, GetDocumentQueryHandler>();

            services.AddScoped<IRequestHandler<GetPhoneQuery, Phone>, GetPhoneQueryHandler>();
            #endregion

            #region
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

#if RABBITMQ
            services.AddHostedService<CustomerRabbitMqService>();
#endif
        }
    }
}
