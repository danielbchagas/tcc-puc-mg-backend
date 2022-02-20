using ECommerce.Ordering.Gateway.Interfaces;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Services.gRPC
{
    public class CustomerGrpcClient : ICustomerGrpcClient
    {
        private readonly ECommerce.Customer.Api.Protos.CustomerService.CustomerServiceClient _client;

        public CustomerGrpcClient(ECommerce.Customer.Api.Protos.CustomerService.CustomerServiceClient client)
        {
            _client = client;
        }

        public async Task<Customer.Api.Protos.GetUserResponse> GetCustomer(Customer.Api.Protos.GetUserRequest request)
        {
            return await _client.GetCustomerAsync(request);
        }
    }
}
