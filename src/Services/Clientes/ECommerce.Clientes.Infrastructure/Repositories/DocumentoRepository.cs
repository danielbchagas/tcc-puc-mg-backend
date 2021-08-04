using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using ECommerce.Clientes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Infrastructure.Repositories
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
    }
}
