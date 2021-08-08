using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using ECommerce.Catalogo.Infrastructure.Data;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Infrastructure.Repositories
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
    }
}
