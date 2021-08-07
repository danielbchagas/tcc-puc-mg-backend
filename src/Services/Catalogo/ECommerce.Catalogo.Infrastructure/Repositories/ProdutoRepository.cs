﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using ECommerce.Catalogo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalogo.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
        }

        public Task Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);

            return Task.CompletedTask;
        }

        public async Task<Produto> Buscar(Guid id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> Buscar(int? pagina, int? linhas)
        {
            if (pagina.HasValue|| linhas.HasValue)
                return await _context.Produtos.AsNoTracking().Skip((pagina.Value - 1) * linhas.Value).Take(linhas.Value).ToListAsync();

            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> Buscar(Expression<Func<Produto, bool>> filtro, int? pagina, int? linhas)
        {
            if (pagina.HasValue || linhas.HasValue)
                return await _context.Produtos.Where(filtro).AsNoTracking().Skip((pagina.Value - 1) * linhas.Value).Take(linhas.Value).ToListAsync();

            return await _context.Produtos.Where(filtro).AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            var produto = await Buscar(id);
            _context.Produtos.Remove(produto);
        }
    }
}