using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models.Request;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Services.gRPC
{
    public class CustomerGrpcClient : ICustomerGrpcClient
    {
        private readonly Customers.Api.Protos.CustomerService.CustomerServiceClient _client;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtHandler;
        private readonly ILogger<CustomerGrpcClient> _logger;

        public CustomerGrpcClient(Customers.Api.Protos.CustomerService.CustomerServiceClient client,
            UserManager<IdentityUser> userManager,
            JwtService jwtHandler,
            ILogger<CustomerGrpcClient> logger)
        {
            _client = client;
            _userManager = userManager;
            _logger = logger;
            _jwtHandler = jwtHandler;
        }

        public async Task<Customers.Api.Protos.CreateUserResponse> Create(CustomerRequest user)
        {
            var token = await _jwtHandler.GenerateNewToken(user.Email.Address);

            var headers = new Metadata
            {
                { "Authorization", $"Bearer {token}" }
            };

            var result = _client.CreateCustomer(
                new Customers.Api.Protos.CreateUserRequest
                {
                    Id = Convert.ToString(user.Id),
                    Firstname = user.FirstName,
                    Lastname = user.LastName,
                    Enabled = true,
                    Document = new Customers.Api.Protos.Document
                    {
                        Number = user.Document.Number,
                        Userid = Convert.ToString(user.Id)
                    },
                    Email = new Customers.Api.Protos.Email
                    {
                        Address = user.Email.Address,
                        Userid = Convert.ToString(user.Id)
                    },
                    Phone = new Customers.Api.Protos.Phone
                    {
                        Number = user.Phone.Number,
                        Userid = Convert.ToString(user.Id)
                    }
                },
                headers: headers
            );

            _logger.LogInformation("Cliente registrado com sucesso.");

            return result;
        }
    }
}
