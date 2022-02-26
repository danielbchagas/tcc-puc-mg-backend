using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICustomerGrpcClient
    {
        Task<Customer.Api.Protos.GetUserResponse> GetCustomer(Customer.Api.Protos.GetUserRequest request);
    }
}
