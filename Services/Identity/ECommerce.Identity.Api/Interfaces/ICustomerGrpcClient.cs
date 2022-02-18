using ECommerce.Identity.Api.Models;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface ICustomerGrpcClient
    {
        Task Create(SignUpUserDto user);
    }
}
