using ECommerce.Customer.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Interfaces
{
    public interface ICustomerGrpcClient
    {
        Task<User> GetCustomer(Guid id);
    }
}
