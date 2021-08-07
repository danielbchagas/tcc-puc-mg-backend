using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using ECommerce.Cliente.Infrastructure.Data;

namespace ECommerce.Cliente.Infrastructure.Repositories
{
    public class LogEventoRepository : ILogEventoRepository
    {
        public LogEventoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(LogEvento log)
        {
            await _context.LogEventos.AddAsync(log);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
