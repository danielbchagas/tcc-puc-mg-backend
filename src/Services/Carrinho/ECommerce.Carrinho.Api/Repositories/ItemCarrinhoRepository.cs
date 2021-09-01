using ECommerce.Carrinho.Api.Data;
using ECommerce.Carrinho.Api.Interfaces.Data;
using ECommerce.Carrinho.Api.Interfaces.Repositories;
using ECommerce.Carrinho.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Repositories
{
    public class ItemCarrinhoRepository : IItemCarrinhoRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemCarrinhoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ItemCarrinho> BuscarPorProdutoId(Guid id)
        {
            return await _context.ItensCarrinhos.FirstOrDefaultAsync(ic => ic.ProdutoId == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task ExcluirPorProdutoId(Guid id)
        {
            var item = await BuscarPorProdutoId(id);

            _context.ItensCarrinhos.Remove(item);
        }
    }
}
