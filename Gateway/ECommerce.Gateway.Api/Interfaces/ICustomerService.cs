using ECommerce.Gateway.Api.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Interfaces
{
    public interface ICustomerService
    {
        [Get("/api/users/{id}")]
        Task<ApiResponse<UserDto>> GetCustomer(Guid id, [Authorize("Bearer")] string accessToken);

        [Put("/api/users")]
        Task<IApiResponse> UpdateCustomer(UserDto userDto, [Authorize("Bearer")] string accessToken);

        [Delete("/api/users/{id}")]
        Task<IApiResponse> DeleteCustomer(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/documents/{id}")]
        Task<ApiResponse<DocumentDto>> GetDocument(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/emails/{id}")]
        Task<ApiResponse<EmailDto>> GetEmail(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/phones/{id}")]
        Task<ApiResponse<PhoneDto>> GetPhone(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/addresses/{id}")]
        Task<ApiResponse<AddressDto>> GetAddress(Guid id, [Authorize("Bearer")] string accessToken);
    }
}
