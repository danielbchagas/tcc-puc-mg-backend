using ECommerce.Customers.Application.Commands.Address;
using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Application.Handlers.Commands.Address;
using ECommerce.Customers.Application.Handlers.Commands.Customer;
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
            services.AddScoped<IRequestHandler<CreateCustomerCommand, (ValidationResult, Customers.Domain.Models.Customer)>, CreateCustomerCommandHandler>();
            services.AddScoped<IRequestHandler<DisableCustomerCommand, ValidationResult>, DisableCustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, (ValidationResult, Customers.Domain.Models.Customer)>, UpdateCustomerCommandHandler>();

            services.AddScoped<IRequestHandler<CreateAddressCommand, (ValidationResult, Address)>, CreateAddressCommandHandler>();
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
        }
    }
}
