using ECommerce.Produtos.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Produtos.Domain.Application.Queries
{
    public class BuscarProdutoPorIdQuery : IRequest<Produto>
    {
        public BuscarProdutoPorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
