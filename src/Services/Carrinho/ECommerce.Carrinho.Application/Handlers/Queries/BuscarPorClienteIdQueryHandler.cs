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
    public class BuscarPorClienteIdQueryHandler : IRequestHandler<BuscarPorClienteIdQuery, Domain.Models.Carrinho>
    {
        public Task<Domain.Models.Carrinho> Handle(BuscarPorClienteIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
