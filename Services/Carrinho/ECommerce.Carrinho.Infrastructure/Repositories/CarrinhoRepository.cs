using ECommerce.Carrinho.Domain.Interfaces.Data;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.CarrinhoCompras;

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

        public async Task Adicionar(CarrinhoCliente carrinho)
        {
            await _context.CarrinhosCompras.AddAsync(carrinho);
        }

        public Task Atualizar(CarrinhoCliente carrinho)
        {
            _context.CarrinhosCompras.Update(carrinho);
            return Task.CompletedTask;
        }

        public async Task<CarrinhoCliente> Buscar(Guid id)
        {
            return await _context.CarrinhosCompras.FindAsync(id);
        }

        public async Task<CarrinhoCliente> BuscarPorClienteId(Guid clienteId)
        {
            return await _context.CarrinhosCompras
                .Include(cc => cc.Itens)
                .FirstOrDefaultAsync(cc => cc.ClienteId == clienteId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            var carrinho = await _context.CarrinhosCompras.FindAsync(id);

            _context.CarrinhosCompras.Remove(carrinho);
        }
    }
}
