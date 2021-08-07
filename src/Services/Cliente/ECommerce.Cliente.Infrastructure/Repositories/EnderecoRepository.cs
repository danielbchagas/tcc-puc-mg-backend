using System;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using ECommerce.Cliente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Cliente.Infrastructure.Repositories
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
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
