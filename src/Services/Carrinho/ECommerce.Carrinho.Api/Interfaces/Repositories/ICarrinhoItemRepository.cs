using ECommerce.Carrinho.Api.Interfaces.Data;
using System;

namespace ECommerce.Carrinho.Api.Interfaces.Repositories
{
    public interface ICarrinhoItemRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
