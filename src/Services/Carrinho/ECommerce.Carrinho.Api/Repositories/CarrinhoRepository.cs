using ECommerce.Carrinho.Api.Data;
using ECommerce.Carrinho.Api.Interfaces.Data;
using ECommerce.Carrinho.Api.Interfaces.Repositories;
using ECommerce.Carrinho.Api.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Carrinho.Api.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Models.Carrinho carrinho)
        {
            await _context.CarrinhosClientes.AddAsync(carrinho);
        }

        public Task Atualizar(Models.Carrinho carrinho)
        {
            _context.CarrinhosClientes.Update(carrinho);
            return Task.CompletedTask;
        }

        public async Task<Models.Carrinho> BuscarPorId(Guid id)
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
