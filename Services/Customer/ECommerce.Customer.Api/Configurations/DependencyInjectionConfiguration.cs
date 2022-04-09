#define REST

using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Application.Handlers.Commands;
using ECommerce.Customer.Application.Handlers.Queries;
using ECommerce.Customer.Application.Queries;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using ECommerce.Customer.Infrastructure.Repositories;
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
            services.AddScoped<IRequestHandler<CreateUserCommand, ValidationResult>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DisableUserCommand, ValidationResult>, DisableUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, ValidationResult>, DeleteUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, ValidationResult>, UpdateUserCommandHandler>();
            
            services.AddScoped<IRequestHandler<CreateAddressCommand, ValidationResult>, CreateAddressCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAddressCommand, ValidationResult>, UpdateAddressCommandHandler>();

            services.AddScoped<IRequestHandler<CreateDocumentCommand, ValidationResult>, CreateDocumentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateDocumentCommand, ValidationResult>, UpdateDocumentCommandHandler>();

            services.AddScoped<IRequestHandler<CreateEmailCommand, ValidationResult>, CreateEmailCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEmailCommand, ValidationResult>, UpdateEmailCommandHandler>();

            services.AddScoped<IRequestHandler<CreatePhoneCommand, ValidationResult>, CreatePhoneCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePhoneCommand, ValidationResult>, UpdatePhoneCommandHandler>();
            #endregion

            #region
            services.AddScoped<IRequestHandler<GetUserQuery, User>, GetUserQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllUsersQuery, IList<User>>, GetAllUsersQueryHandler>();

            services.AddScoped<IRequestHandler<GetAddressQuery, Address>, GetAddressQueryHandler>();

            services.AddScoped<IRequestHandler<GetEmailQuery, Email>, GetEmailQueryHandler>();

            services.AddScoped<IRequestHandler<GetDocumentQuery, Document>, GetDocumentQueryHandler>();

            services.AddScoped<IRequestHandler<GetPhoneQuery, Phone>, GetPhoneQueryHandler>();
            #endregion

            #region
            services.AddScoped<IUserRepository, UserRepository>();
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
