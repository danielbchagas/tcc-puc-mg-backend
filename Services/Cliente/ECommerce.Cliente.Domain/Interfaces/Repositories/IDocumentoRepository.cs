using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Models;

namespace ECommerce.Cliente.Domain.Interfaces.Repositories
{
    public interface IDocumentoRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Documento documento);
        Task Atualizar(Documento documento);
        Task<Documento> Buscar(Guid id);
        Task<IEnumerable<Documento>> Buscar(Expression<Func<Documento, bool>> filtro);
    }
}
