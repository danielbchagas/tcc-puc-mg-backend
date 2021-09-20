using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Queries
{
    public class BuscarPorClienteIdQuery : IRequest<Domain.Models.Carrinho>
    {
        public BuscarPorClienteIdQuery(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public Guid ClienteId { get; set; }
    }
}
