using Dominio = ECommerce.Clientes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Clientes.Domain.Application.Queries.Cliente
{
    public class BuscarClientesFiltradosPaginadosQuery : IRequest<IEnumerable<Dominio.Cliente>>
    {
        public Expression<Func<Dominio.Cliente, bool>> Filtro { get; set; }
        public int? Pagina { get; set; }
        public int? Linhas { get; set; }
    }
}
