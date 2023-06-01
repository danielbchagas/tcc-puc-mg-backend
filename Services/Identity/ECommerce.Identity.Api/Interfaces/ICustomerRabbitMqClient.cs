using ECommerce.Identity.Api.Models.Request;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface ICustomerRabbitMqClient
    {
        Task CreateCustomer(CustomerRequest request);
    }
}
