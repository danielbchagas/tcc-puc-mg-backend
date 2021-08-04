using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using ECommerce.Clientes.Infrastructure.Data;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Infrastructure.Repositories
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
