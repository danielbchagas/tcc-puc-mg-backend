using ECommerce.Customer.Api.Services.RabbitMQ;
using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Application.Handlers.Commands.Customer;
using ECommerce.Customers.Application.Handlers.Queries;
using ECommerce.Customers.Application.Queries;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Infrastructure.Data;
using ECommerce.Customers.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Customer.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            #region Commands
            services.AddScoped<IRequestHandler<CreateCustomerCommand, (ValidationResult, Customers.Domain.Models.Customer)>, CreateCustomerCommandHandler>();
            services.AddScoped<IRequestHandler<DisableCustomerCommand, ValidationResult>, DisableCustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, (ValidationResult, Customers.Domain.Models.Customer)>, UpdateCustomerCommandHandler>();
            #endregion

            #region Queries
            services.AddScoped<IRequestHandler<GetUserQuery, Customers.Domain.Models.Customer>, GetCustomerQueryHandler>();
            #endregion

            #region Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            services.AddHostedService<CustomerRabbitMqService>();
        }
    }
}
