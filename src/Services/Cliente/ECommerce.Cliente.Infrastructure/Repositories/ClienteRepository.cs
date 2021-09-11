using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Cliente.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Domain.Models.Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
        }

        public async Task Atualizar(Domain.Models.Cliente cliente)
        {
            _context.Clientes.Update(cliente);

            await Task.CompletedTask;
        }

        public async Task<Domain.Models.Cliente> Buscar(Guid id)
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .Include(c => c.Telefone)
                .Where(c => c.Ativo == true)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Domain.Models.Cliente>> Buscar(int pagina, int linhas)
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .Include(c => c.Telefone)
                .AsNoTracking()
                .Where(c => c.Ativo == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Models.Cliente>> Buscar(Expression<Func<Domain.Models.Cliente, bool>> filtro, int pagina, int linhas)
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .Include(c => c.Telefone)
                .AsNoTracking()
                .Where(c => c.Ativo == true)
                .Where(filtro)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            var cliente = await Buscar(id);
            _context.Clientes.Remove(cliente);
        }

        public async Task<IEnumerable<Domain.Models.Cliente>> Buscar()
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .Include(c => c.Telefone)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Models.Cliente>> Buscar(Expression<Func<Domain.Models.Cliente, bool>> filtro)
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .Include(c => c.Telefone)
                .Where(filtro)
                .ToListAsync();
        }
    }
}
