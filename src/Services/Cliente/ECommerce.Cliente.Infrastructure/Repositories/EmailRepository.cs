using System;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Interfaces.Data;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using ECommerce.Cliente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Cliente.Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        public EmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Email email)
        {
            await _context.Emails.AddAsync(email);
        }

        public async Task Atualizar(Email email)
        {
            _context.Emails.Update(email);
            await Task.CompletedTask;
        }

        public async Task<Email> Buscar(Guid id)
        {
            return await _context.Emails
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
