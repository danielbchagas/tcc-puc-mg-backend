using ECommerce.Identity.Api.DTOs.Request;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface ICustomerGrpcClient
    {
        Task<ECommerce.Customer.Api.Protos.CreateUserResponse> Create(SignUpUserRequest user);
    }
}
