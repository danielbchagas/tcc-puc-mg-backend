﻿using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using ECommerce.Clientes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);

            await Task.CompletedTask;
        }

        public async Task<Cliente> Buscar(Guid id)
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .Where(c => c.Ativo == true)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> Buscar(int pagina, int linhas)
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .AsNoTracking()
                .Where(c => c.Ativo == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> filtro, int pagina, int linhas)
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .AsNoTracking()
                .Where(c => c.Ativo == true)
                .Where(filtro)
                .ToListAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task Excluir(Guid id)
        {
            var cliente = await Buscar(id);
            _context.Clientes.Remove(cliente);
        }

        public async Task<IEnumerable<Cliente>> Buscar()
        {
            return await _context.Clientes
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Endereco)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
