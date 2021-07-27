using ECommerce.Clientes.Domain.Application.Queries.Endereco;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries.Endereco
{
    public class BuscarEnderecosPaginadosQueryHandler : IRequestHandler<BuscarEnderecosPaginadosQuery, IEnumerable<Dominio.Endereco>>
    {
        public BuscarEnderecosPaginadosQueryHandler(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        private readonly IEnderecoRepository _repository;

        public async Task<IEnumerable<Dominio.Endereco>> Handle(BuscarEnderecosPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Pagina, request.Linhas);
        }
    }
}
