using System;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
{
    public class GetTelefoneQuery : IRequest<Telefone>
    {
        public GetTelefoneQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
