using ECommerce.Catalogo.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Catalogo.Application.Queries
{
    public class GetProdutoQuery : IRequest<Produto>
    {
        public GetProdutoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
