using ECommerce.Carrinho.Domain.Interfaces.Data;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Domain.Models;
using ECommerce.Carrinho.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Infrastructure.Repositories
{
    public class ItemCarrinhoRepository : IItemCarrinhoRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemCarrinhoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(CarrinhoItem item)
        {
            await _context.Itens.AddAsync(item);
        }

        public Task Atualizar(CarrinhoItem item)
        {
            _context.Itens.Update(item);

            return Task.CompletedTask;
        }

        public async Task<CarrinhoItem> BuscarPorProdutoId(Guid id)
        {
            return await _context.Itens.FirstOrDefaultAsync(ic => ic.ProdutoId == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            var item = await _context.Itens.FindAsync(id);

            _context.Itens.Remove(item);
        }
    }
}
