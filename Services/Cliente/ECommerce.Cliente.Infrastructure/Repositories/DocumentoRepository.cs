﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using ECommerce.Cliente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Cliente.Infrastructure.Repositories
{
    public class DocumentoRepository : IDocumentoRepository
    {
        public DocumentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Documento documento)
        {
            await _context.Documentos.AddAsync(documento);
        }

        public async Task Atualizar(Documento documento)
        {
            _context.Documentos.Update(documento);
            await Task.CompletedTask;
        }

        public async Task<Documento> Buscar(Guid id)
        {
            return await _context.Documentos
                .Include(d => d.Cliente)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<IEnumerable<Documento>> Buscar(Expression<Func<Documento, bool>> filtro)
        {
            return await _context.Documentos
                .Include(d => d.Cliente)
                .Where(filtro)
                .ToListAsync();
        }
    }
}