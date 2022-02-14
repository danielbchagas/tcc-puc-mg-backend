using ECommerce.Customer.Domain.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICustomerService
    {
        [Get("/api/user/{id}")]
        Task<ApiResponse<User>> GetCustomer(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/document/{id}")]
        Task<ApiResponse<Document>> GetDocument(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/email/{id}")]
        Task<ApiResponse<Email>> GetEmail(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/phone/{id}")]
        Task<ApiResponse<Phone>> GetPhone(Guid id, [Authorize("Bearer")] string accessToken);

        [Get("/api/address/{id}")]
        Task<ApiResponse<Address>> GetAddress(Guid id, [Authorize("Bearer")] string accessToken);
    }
}
