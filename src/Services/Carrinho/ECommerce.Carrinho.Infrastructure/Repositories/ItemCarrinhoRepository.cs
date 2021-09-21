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

        public async Task Adicionar(ItemCarrinho item)
        {
            await _context.ItensCarrinhos.AddAsync(item);
        }

        public Task Atualizar(ItemCarrinho item)
        {
            _context.ItensCarrinhos.Update(item);

            return Task.CompletedTask;
        }

        public async Task<ItemCarrinho> BuscarPorProdutoId(Guid id)
        {
            return await _context.ItensCarrinhos.FirstOrDefaultAsync(ic => ic.ProdutoId == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            var item = await _context.ItensCarrinhos.FindAsync(id);

            _context.ItensCarrinhos.Remove(item);
        }
    }
}
