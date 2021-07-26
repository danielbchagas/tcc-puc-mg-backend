﻿using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(Cliente produto);
        Task Atualizar(Cliente produto);
        Task Excluir(Guid id);
        Task<Cliente> Buscar(Guid id);
        Task<IEnumerable<Cliente>> Buscar(int? pagina, int? linhas);
        Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> filtro, int? pagina, int? linhas);
    }
}