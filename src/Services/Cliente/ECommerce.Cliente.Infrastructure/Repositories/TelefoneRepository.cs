using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using ECommerce.Cliente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Infrastructure.Repositories
{
    public class TelefoneRepository : ITelefoneRepository
    {
        public TelefoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Telefone telefone)
        {
            await _context.Telefones.AddAsync(telefone);
        }

        public Task Atualizar(Telefone telefone)
        {
            _context.Telefones.Update(telefone);
            return Task.CompletedTask;
        }

        public async Task<Telefone> Buscar(Guid id)
        {
            return await _context.Telefones
                .Include(t => t.Cliente)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<IEnumerable<Telefone>> Buscar(Expression<Func<Telefone, bool>> filtro)
        {
            return await _context.Telefones
                .Include(t => t.Cliente)
                .Where(filtro)
                .ToListAsync();
        }
    }
}
