using ECommerce.Clientes.Domain.Application.Queries.Endereco;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries.Endereco
{
    public class BuscarEnderecosFiltradosPaginadosQueryHandler : IRequestHandler<BuscarEnderecosFiltradosPaginadosQuery, IEnumerable<Dominio.Endereco>>
    {
        public Task<IEnumerable<Dominio.Endereco>> Handle(BuscarEnderecosFiltradosPaginadosQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
