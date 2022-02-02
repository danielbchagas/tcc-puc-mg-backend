using System.Threading.Tasks;
using ECommerce.Identity.Api.Models;
using Refit;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface ICustomerService
    {
        [Post("/api/user")]
        Task<ApiResponse<object>> Create([Body]UserDto user, [Authorize("Bearer")] string accessToken);
    }
}
