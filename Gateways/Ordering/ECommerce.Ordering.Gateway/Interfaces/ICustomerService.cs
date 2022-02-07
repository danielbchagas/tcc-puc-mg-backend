using ECommerce.Core.Models.Customer;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICustomerService
    {
        #region User
        [Get("/api/user/{id}")]
        Task<ApiResponse<User>> GetCustomer(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Document
        [Get("/api/document/{id}")]
        Task<ApiResponse<Document>> GetDocument(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Email
        [Get("/api/email/{id}")]
        Task<ApiResponse<Email>> GetEmail(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Phone
        [Get("/api/phone/{id}")]
        Task<ApiResponse<Phone>> GetPhone(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion

        #region Address
        [Get("/api/address/{id}")]
        Task<ApiResponse<Address>> GetAddress(Guid id, [Authorize("Bearer")] string accessToken);
        #endregion
    }
}
