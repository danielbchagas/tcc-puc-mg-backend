using ECommerce.Carrinho.Api.Data;
using ECommerce.Carrinho.Api.Interfaces.Data;
using ECommerce.Carrinho.Api.Interfaces.Repositories;
using ECommerce.Carrinho.Api.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Carrinho.Api.Repositories
{
    public class CarrinhoClienteRepository : ICarrinhoClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(CarrinhoCliente carrinho)
        {
            await _context.CarrinhosClientes.AddAsync(carrinho);
        }

        public Task Atualizar(CarrinhoCliente carrinho)
        {
            _context.CarrinhosClientes.Update(carrinho);
            return Task.CompletedTask;
        }

        public async Task<CarrinhoCliente> BuscarPorId(Guid id)
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
