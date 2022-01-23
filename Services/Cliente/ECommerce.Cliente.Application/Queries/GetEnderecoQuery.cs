using System;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
{
    public class GetEnderecoQuery : IRequest<Endereco>
    {
        public GetEnderecoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
