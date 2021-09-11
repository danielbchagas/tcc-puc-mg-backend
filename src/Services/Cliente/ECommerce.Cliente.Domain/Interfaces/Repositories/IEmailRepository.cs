using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Models;

namespace ECommerce.Cliente.Domain.Interfaces.Repositories
{
    public interface IEmailRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Email email);
        Task Atualizar(Email email);
        Task<Email> Buscar(Guid id);
        Task<IEnumerable<Email>> Buscar(Expression<Func<Email, bool>> filtro);
    }
}
