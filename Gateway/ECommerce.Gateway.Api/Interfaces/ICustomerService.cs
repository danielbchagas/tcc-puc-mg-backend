using ECommerce.Gateway.Api.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Interfaces
{
    public interface ICustomerService
    {
        [Get("/api/customers/{id}")]
        Task<ApiResponse<CustomerDto>> GetCustomer(Guid id, [Authorize("Bearer")] string accessToken);

        [Put("/api/customers")]
        Task<IApiResponse> UpdateCustomer(CustomerDto customerDto, [Authorize("Bearer")] string accessToken);

        [Delete("/api/customers/{id}")]
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
