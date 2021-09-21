using ECommerce.Carrinho.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Application.Handlers.Queries
{
    public class BuscarCarrinhoPorClienteQueryHandler : IRequestHandler<BuscarCarrinhoPorClienteQuery, Domain.Models.Carrinho>
    {
        public Task<Domain.Models.Carrinho> Handle(BuscarCarrinhoPorClienteQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
