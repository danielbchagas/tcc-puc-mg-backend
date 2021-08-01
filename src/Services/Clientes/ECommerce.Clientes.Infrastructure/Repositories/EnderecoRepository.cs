using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using ECommerce.Clientes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public EnderecoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
        }

        public async Task Atualizar(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);

            await Task.CompletedTask;
        }

        public async Task<Endereco> Buscar(Guid id)
        {
            return await _context.Enderecos
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Endereco>> Buscar(int? pagina, int? linhas)
        {
            if (pagina.HasValue && linhas.HasValue)
                return await _context.Enderecos
                    .Include(c => c.Cliente)
                    .AsNoTracking()
                    .Skip((pagina.Value - 1) * linhas.Value)
                    .Take(linhas.Value)
                    .ToListAsync();

            return await _context.Enderecos.Include(c => c.Cliente).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Endereco>> Buscar(Expression<Func<Endereco, bool>> filtro, int? pagina, int? linhas)
        {
            if (pagina.HasValue && linhas.HasValue)
                return await _context.Enderecos
                    .Include(c => c.Cliente)
                    .AsNoTracking()
                    .Where(filtro)
                    .Skip((pagina.Value - 1) * linhas.Value)
                    .Take(linhas.Value)
                    .ToListAsync();

            return await _context.Enderecos
                .Include(c => c.Cliente)
                .AsNoTracking()
                .Where(filtro)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            var endereco = await Buscar(id);
            _context.Enderecos.Remove(endereco);
        }
    }
}
