using ECommerce.Ordering.Gateway.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICustomerService
    {
        #region User
        [Get("/api/user/{id}")]
        Task<ApiResponse<CustomerDto>> GetCustomer(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Document
        [Get("/api/document/{id}")]
        Task<ApiResponse<DocumentDto>> GetDocument(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Email
        [Get("/api/email/{id}")]
        Task<ApiResponse<EmailDto>> GetEmail(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Phone
        [Get("/api/phone/{id}")]
        Task<ApiResponse<PhoneDto>> GetPhone(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Address
        [Get("/api/address/{id}")]
        Task<ApiResponse<AddressDto>> GetAddress(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion
    }
}
