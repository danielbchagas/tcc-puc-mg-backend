using System.Threading.Tasks;

namespace ECommerce.Common.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
