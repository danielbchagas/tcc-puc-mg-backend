using System;
using System.Threading.Tasks;
using ECommerce.Ordering.Gateway.Models;
using Refit;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICustomerService
    {
        [Get("/api/user/{id}")]
        Task<ApiResponse<CustomerDto>> GetCustomer(Guid id, [Authorize("Bearer")] string accessToken);

        [Put("/api/user")]
        Task<IApiResponse> UpdateCustomer(CustomerDto customer, [Authorize("Bearer")] string accessToken);

        [Delete("/api/user/{id}")]
        Task<IApiResponse> DeleteCustomer(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/document/{id}")]
        Task<ApiResponse<DocumentDto>> GetDocument(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/email/{id}")]
        Task<ApiResponse<EmailDto>> GetEmail(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/phone/{id}")]
        Task<ApiResponse<PhoneDto>> GetPhone(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/address/{id}")]
        Task<ApiResponse<AddressDto>> GetAddress(Guid id, [Authorize("Bearer")] string accessToken);
    }
}
