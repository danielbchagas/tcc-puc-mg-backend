using ECommerce.Carrinho.Domain.Interfaces.Data;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Infrastructure.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Domain.Models.Carrinho carrinho)
        {
            await _context.CarrinhosClientes.AddAsync(carrinho);
        }

        public Task Atualizar(Domain.Models.Carrinho carrinho)
        {
            _context.CarrinhosClientes.Update(carrinho);
            return Task.CompletedTask;
        }

        public async Task<Domain.Models.Carrinho> BuscarPorId(Guid id)
        {
            return await _context.CarrinhosClientes
                .Include(cc => cc.Itens)
                .FirstOrDefaultAsync(cc => cc.ClienteId == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
