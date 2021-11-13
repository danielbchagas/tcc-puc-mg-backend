using ECommerce.Carrinho.Application.Queries;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Carrinho;

namespace ECommerce.Carrinho.Application.Handlers.Queries
{
    public class BuscarCarrinhoPorClienteQueryHandler : IRequestHandler<BuscarCarrinhoPorClienteQuery, CarrinhoCliente>
    {
        private readonly ICarrinhoRepository _repository;

        public BuscarCarrinhoPorClienteQueryHandler(ICarrinhoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CarrinhoCliente> Handle(BuscarCarrinhoPorClienteQuery request, CancellationToken cancellationToken)
        {
            var carrinho = await _repository.Buscar(request.ClienteId);

            return await Task.FromResult(carrinho);
        }
    }
}
