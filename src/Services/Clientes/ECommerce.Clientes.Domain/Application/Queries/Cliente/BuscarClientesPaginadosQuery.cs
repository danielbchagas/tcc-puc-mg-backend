using Dominio = ECommerce.Clientes.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace ECommerce.Clientes.Domain.Application.Queries.Cliente
{
    public class BuscarClientesPaginadosQuery : IRequest<IEnumerable<Dominio.Cliente>>
    {
        public BuscarClientesPaginadosQuery(int? pagina, int? linhas)
        {
            Pagina = pagina;
            Linhas = linhas;
        }

        public int? Pagina { get; set; }
        public int? Linhas { get; set; }
    }
}
