using ECommerce.Identity.Api.Models;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface ICustomerGrpcClient
    {
        Task<ECommerce.Customer.Api.Protos.CreateUserResponse> Create(SignUpUserDto user);
    }
}
