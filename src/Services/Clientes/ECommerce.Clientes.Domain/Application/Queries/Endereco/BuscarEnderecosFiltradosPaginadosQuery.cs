using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Queries.Endereco
{
    public class BuscarEnderecosFiltradosPaginadosQuery : IRequest<IEnumerable<Dominio.Endereco>>
    {
        public Expression<Func<Dominio.Endereco, bool>> Filtro { get; set; }
        public int? Pagina { get; set; }
        public int? Linhas { get; set; }
    }
}
