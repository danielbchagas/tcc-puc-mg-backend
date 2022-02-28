using ECommerce.Identity.Api.Handlers;
using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Services.gRPC
{
    public class CustomerGrpcClient : ICustomerGrpcClient
    {
        private readonly ECommerce.Customer.Api.Protos.CustomerService.CustomerServiceClient _client;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly ILogger<CustomerGrpcClient> _logger;

        public CustomerGrpcClient(ECommerce.Customer.Api.Protos.CustomerService.CustomerServiceClient client,
            UserManager<IdentityUser> userManager,
            JwtHandler jwtHandler,
            ILogger<CustomerGrpcClient> logger)
        {
            _client = client;
            _userManager = userManager;
            _logger = logger;
            _jwtHandler = jwtHandler;
        }

        public async Task<ECommerce.Customer.Api.Protos.CreateUserResponse> Create(SignUpUserDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            var token = await _jwtHandler.GenerateNewToken(user.Email);

            var headers = new Metadata
            {
                { "Authorization", $"Bearer {token}" }
            };

            var result = _client.CreateCustomer(
                new Customer.Api.Protos.CreateUserRequest
                {
                    Id = Convert.ToString(identityUser.Id),
                    Firstname = user.FirstName,
                    Lastname = user.LastName,
                    Enabled = true,
                    Document = new ECommerce.Customer.Api.Protos.Document
                    {
                        Number = user.Document,
                        Userid = Convert.ToString(identityUser.Id)
                    },
                    Email = new ECommerce.Customer.Api.Protos.Email
                    {
                        Address = user.Email,
                        Userid = Convert.ToString(identityUser.Id)
                    },
                    Phone = new ECommerce.Customer.Api.Protos.Phone
                    {
                        Number = user.Phone,
                        Userid = Convert.ToString(identityUser.Id)
                    }
                }, 
                headers: headers
            );

            _logger.LogInformation("Cliente registrado com sucesso.");

            return result;
        }
    }
}
