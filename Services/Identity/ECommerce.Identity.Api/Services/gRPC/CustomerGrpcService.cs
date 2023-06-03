using ECommerce.Identity.Api.Handler;
using ECommerce.Identity.Api.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Services.gRPC
{
    public class CustomerGrpcService : ICustomerGrpcClient
    {
        private readonly Customers.Api.Protos.CustomerService.CustomerServiceClient _client;
        private readonly JwtHandler _jwtHandler;
        private readonly ILogger<CustomerGrpcService> _logger;
        
        public CustomerGrpcService(Customers.Api.Protos.CustomerService.CustomerServiceClient client,
            JwtHandler jwtHandler,
            ILogger<CustomerGrpcService> logger)
        {
            _client = client;
            _logger = logger;
            _jwtHandler = jwtHandler;
        }

        public async Task<Customers.Api.Protos.CreateUserResponse> Create(Customers.Api.Protos.CreateUserRequest user)
        {
            var token = await _jwtHandler.GenerateNewToken(user.Email.Address);

            var headers = new Metadata
            {
                { "Authorization", $"Bearer {token}" }
            };

            var result = _client.CreateCustomer(
                user,
                headers: headers
            );

            _logger.LogInformation("Cliente registrado com sucesso.");

            return result;
        }
    }
}
