using MediatR;
using System.Collections.Generic;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Queries.Endereco
{
    public class BuscarEnderecosPaginadosQuery : IRequest<IEnumerable<Dominio.Endereco>>
    {
        public BuscarEnderecosPaginadosQuery(int? pagina, int? linhas)
        {
            Pagina = pagina;
            Linhas = linhas;
        }

        public int? Pagina { get; set; }
        public int? Linhas { get; set; }
    }
}
