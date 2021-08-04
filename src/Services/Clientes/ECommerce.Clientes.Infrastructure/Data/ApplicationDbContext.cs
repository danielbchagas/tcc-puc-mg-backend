using ECommerce.Clientes.Domain.Interfaces.Data;
using ECommerce.Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<LogEvento> LogEventos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Debugger.IsAttached)
                optionsBuilder
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableSensitiveDataLogging();

            //if (!optionsBuilder.IsConfigured)
            //    optionsBuilder.UseSqlServer("Server=localhost;Database=ClientesDB;User Id=sa;Password=yourStrong(!)Password;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Mapeamento
            modelBuilder.Entity<Cliente>(c =>
            {
                c.ToTable("Clientes");

                c.HasKey(c => c.Id);

                c.Property(c => c.Nome).HasColumnType("varchar(50)").IsRequired();
                c.Property(c => c.Sobrenome).HasColumnType("varchar(100)").IsRequired();
                c.Property(c => c.DataNascimento).HasColumnType("date").IsRequired();
                c.Property(c => c.Ativo).HasColumnType("bit").IsRequired();

                c.HasOne(c => c.Documento).WithOne(d => d.Cliente).OnDelete(DeleteBehavior.Cascade);
                c.HasOne(c => c.Endereco).WithOne(e => e.Cliente).OnDelete(DeleteBehavior.Cascade);
                c.HasOne(c => c.Email).WithOne(e => e.Cliente).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Documento>(d =>
            {
                d.ToTable("Documentos");

                d.HasKey(d => d.Id);

                d.Property(d => d.Numero).HasColumnType("varchar(18)").IsRequired();
                d.Property(e => e.ClienteId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            });

            modelBuilder.Entity<Endereco>(e => 
            {
                e.ToTable("Enderecos");

                e.HasKey(e => e.Id);

                e.Property(e => e.Logradouro).HasColumnType("varchar(200)").IsRequired();
                e.Property(e => e.Bairro).HasColumnType("varchar(50)").IsRequired();
                e.Property(e => e.Cidade).HasColumnType("varchar(50)").IsRequired();
                e.Property(e => e.Cep).HasColumnType("varchar(9)").IsRequired();
                e.Property(e => e.Estado).HasColumnType("char(2)").IsRequired();
                e.Property(e => e.ClienteId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            });

            modelBuilder.Entity<Email>(e => 
            {
                e.ToTable("Emails");

                e.HasKey(e => e.Id);

                e.Property(e => e.Endereco).HasColumnType("varchar(100)").IsRequired();
                e.Property(e => e.ClienteId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            });

            modelBuilder.Entity<LogEvento>(le => 
            {
                le.ToTable("LogEventos");

                le.HasKey(le => le.Id);

                le.Property(le => le.Momento).HasColumnType("date").IsRequired();
                le.Property(l => l.ClienteId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
                le.Property(l => l.UsuarioId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            });
            #endregion

            #region Seed
            var clienteDaviGiovanniFelipe = 
                new Cliente(nome: "Davi Giovanni Felipe", sobrenome: "Fernandes", dataNascimento: new DateTime(1955, 02, 07));
            var documentoDaviGiovanniFelipe = 
                new Documento(numero: "903.142.734-92", clienteId: clienteDaviGiovanniFelipe.Id);
            var enderecoDaviGiovanniFelipe = 
                new Endereco(logradouro: "Colônia Agrícola Águas Claras Chácara 23, 641", bairro: "Guará I", cidade: "Brasília", cep: "71090-265", estado: Estados.DF, clienteId: clienteDaviGiovanniFelipe.Id);
            var emailDaviGiovanniFelipe = 
                new Email(endereco: "davi_giovanni_felipe@gmail.com", clienteDaviGiovanniFelipe.Id);

            var clienteAylaCarolineAnaGomes = 
                new Cliente(nome: "Ayla Caroline", sobrenome: "Ana Gomes", dataNascimento: new DateTime(1963, 12, 12));
            var documentoAylaCarolineAnaGomes = 
                new Documento(numero: "668.154.787-77", clienteId: clienteAylaCarolineAnaGomes.Id);
            var enderecoAylaCarolineAnaGomes = 
                new Endereco(logradouro: "Praça São Francisco de Assis, 442", bairro: "Tarumã", cidade: "Curitiba", cep: "82530-220", estado: Estados.PR, clienteId: clienteAylaCarolineAnaGomes.Id);
            var emailAylaCarolineAnaGomes = 
                new Email(endereco: "ayla_caroline_ana_gomes@gmail.com", clienteAylaCarolineAnaGomes.Id);

            var clienteBetinaFláviaSouza = 
                new Cliente(nome: "BetinaFlávia", sobrenome: "Souza", dataNascimento: new DateTime(1975, 02, 16));
            var documentoBetinaFláviaSouza = 
                new Documento(numero: "345.712.047-10", clienteId: clienteBetinaFláviaSouza.Id);
            var enderecoBetinaFláviaSouza = 
                new Endereco(logradouro: "Rua Neves, 378", bairro: "Abegay", cidade: "Cruz Alta", cep: "98045-115", estado: Estados.RS, clienteId: clienteBetinaFláviaSouza.Id);
            var emailBetinaFláviaSouza = 
                new Email(endereco: "b_etina_flavia_souza@gmail.com", clienteBetinaFláviaSouza.Id);

            var usuarioId = Guid.NewGuid();

            modelBuilder.Entity<Cliente>().HasData(
                clienteDaviGiovanniFelipe, 
                clienteAylaCarolineAnaGomes, 
                clienteBetinaFláviaSouza
            );
            modelBuilder.Entity<Documento>().HasData(
                documentoDaviGiovanniFelipe, 
                documentoAylaCarolineAnaGomes, 
                documentoBetinaFláviaSouza
            );
            modelBuilder.Entity<Endereco>().HasData(
                enderecoDaviGiovanniFelipe, 
                enderecoAylaCarolineAnaGomes, 
                enderecoBetinaFláviaSouza
            );

            modelBuilder.Entity<Email>().HasData(
                 emailDaviGiovanniFelipe,
                 emailAylaCarolineAnaGomes,
                 emailBetinaFláviaSouza
            );

            modelBuilder.Entity<LogEvento>().HasData(
                new LogEvento(clienteId: clienteDaviGiovanniFelipe.Id, usuarioId: usuarioId),
                new LogEvento(clienteId: clienteAylaCarolineAnaGomes.Id, usuarioId: usuarioId),
                new LogEvento(clienteId: clienteBetinaFláviaSouza.Id, usuarioId: usuarioId)
            );
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
