using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Resources;

namespace ECommerce.Catalogo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext() 
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options) 
        {
            
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<LogEvento> LogEventos { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Debugger.IsAttached)
                optionsBuilder.LogTo(Console.WriteLine);

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=CatalogoDB;User Id=sa;Password=yourStrong(!)Password;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Mapeamento
            modelBuilder.Entity<Produto>(p => 
            {
                p.ToTable("Produtos");

                p.HasKey(p => p.Id);

                p.Property(p => p.Marca).HasColumnType("varchar(50)").IsRequired();
                p.Property(p => p.Nome).HasColumnType("varchar(50)").IsRequired();
                p.Property(p => p.Observacao).HasColumnType("varchar(200)").IsRequired(false);
                p.Property(p => p.Quantidade).HasColumnType("int").IsRequired();
                p.Property(p => p.Imagem).HasColumnType("text").IsRequired(false);
                p.Property(p => p.Lote).HasColumnType("varchar(10)").IsRequired(false);
                p.Property(p => p.Ativo).HasColumnType("bit").IsRequired();
                p.Property(p => p.Preco).HasColumnType("money").IsRequired();
            });

            modelBuilder.Entity<LogEvento>(le =>
            {
                le.ToTable("LogEventos");

                le.HasKey(le => le.Id);

                le.Property(le => le.Momento).HasColumnType("date").IsRequired();
                le.Property(l => l.EntidadeId).HasColumnType("uniqueidentifier").IsRequired();
                le.Property(l => l.UsuarioId).HasColumnType("uniqueidentifier").IsRequired();
            });
            #endregion

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            var rm = new ResourceManager(typeof(ImagensResource));

            var imagemCamisaManUtd = Convert.ToBase64String((byte[])rm.GetObject("camisa_man_utd"));
            var imagemJaquetaManUtd = Convert.ToBase64String((byte[])rm.GetObject("jaqueta_man_utd"));
            var imagemCanecaManUtd = Convert.ToBase64String((byte[])rm.GetObject("caneca_man_utd"));
            var imagemBoneManUtd = Convert.ToBase64String((byte[])rm.GetObject("bone_man_utd"));
            var imagemBermudaAdidas = Convert.ToBase64String((byte[])rm.GetObject("bermuda_adidas"));
            var imagemBoneAdidas = Convert.ToBase64String((byte[])rm.GetObject("bone_adidas"));
            var imagemMeiaAdidas = Convert.ToBase64String((byte[])rm.GetObject("meia_adidas"));
            var imagemTenisAdidas = Convert.ToBase64String((byte[])rm.GetObject("tenis_adidas"));
            var imagemCamisaGreenBayPackers = Convert.ToBase64String((byte[])rm.GetObject("camisa_green_bay_packers"));
            var imagemBoneGreenBayPackers = Convert.ToBase64String((byte[])rm.GetObject("bone_green_bay_packers"));
            var imagemCamisetaDcShoes = Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes"));
            var imagemBermudaDcShoes = Convert.ToBase64String((byte[])rm.GetObject("bermuda_dc_shoes"));

            var produtoCamisetaManUtd = new Produto(
                marca: "Manchester United Football Club",
                nome: "Camisete",
                lote: null,
                imagem: imagemCamisaManUtd,
                observacao: null,
                quantidade: 100,
                preco: 200.50m
            );
            var produtoJaquetaManUtd = new Produto(
                marca: "Manchester United Football Club",
                nome: "Jaqueta",
                lote: null,
                imagem: imagemJaquetaManUtd,
                observacao: null,
                quantidade: 250,
                preco: 300.50m
            );
            var produtoBoneManUtd = new Produto(
                marca: "Manchester United Football Club",
                nome: "Boné",
                lote: null,
                imagem: imagemBoneManUtd,
                observacao: null,
                quantidade: 10,
                preco: 80.50m
            );
            var produtoBermudaAdidas = new Produto(
                marca: "Adidas",
                nome: "Bermuda",
                lote: null,
                imagem: imagemBermudaAdidas,
                observacao: null,
                quantidade: 10,
                preco: 150.50m
            );
            var produtoBermudaDcShoes = new Produto(
                marca: "Dc Shoes",
                nome: "Bermuda",
                lote: null,
                imagem: imagemBermudaDcShoes,
                observacao: null,
                quantidade: 10,
                preco: 150.50m
            );
            var produtoCamisetaDcShoes = new Produto(
                marca: "Dc Shoes",
                nome: "Camiseta",
                lote: null,
                imagem: imagemCamisetaDcShoes,
                observacao: null,
                quantidade: 10,
                preco: 100.50m
            );
            var produtoBoneAdidas = new Produto(
                marca: "Adidas",
                nome: "Bone",
                lote: null,
                imagem: imagemBoneAdidas,
                observacao: null,
                quantidade: 10,
                preco: 60.50m
            );
            var produtoTenisAdidas = new Produto(
                marca: "Adidas",
                nome: "Tenis",
                lote: null,
                imagem: imagemTenisAdidas,
                observacao: null,
                quantidade: 10,
                preco: 350.50m
            );
            var produtoCamisaGreenBayPackers = new Produto(
                marca: "Green Bay Packers",
                nome: "Camisa",
                lote: null,
                imagem: imagemCamisaGreenBayPackers,
                observacao: null,
                quantidade: 10,
                preco: 400.50m
            );
            var produtoBoneGreenBayPackers = new Produto(
                marca: "Green Bay Packers",
                nome: "Bone",
                lote: null,
                imagem: imagemBoneGreenBayPackers,
                observacao: null,
                quantidade: 10,
                preco: 150.50m
            );
            var produtoCanecaManUtd = new Produto(
                marca: "Manchester United Football Club",
                nome: "Caneca",
                lote: null,
                imagem: imagemCanecaManUtd,
                observacao: null,
                quantidade: 10,
                preco: 30.50m
            );
            var produtoMeiaAdidas = new Produto(
                marca: "Adidas",
                nome: "Meia",
                lote: null,
                imagem: imagemMeiaAdidas,
                observacao: null,
                quantidade: 10,
                preco: 20.50m
            );

            var usuarioId = Guid.NewGuid();

            modelBuilder.Entity<Produto>().HasData(
                produtoCamisetaManUtd,
                produtoJaquetaManUtd,
                produtoBoneManUtd,
                produtoBermudaAdidas,
                produtoBermudaDcShoes,
                produtoCamisetaDcShoes,
                produtoBoneAdidas,
                produtoTenisAdidas,
                produtoCamisaGreenBayPackers,
                produtoBoneGreenBayPackers,
                produtoCanecaManUtd,
                produtoMeiaAdidas
            );

            modelBuilder.Entity<LogEvento>().HasData(
                new LogEvento(entidadeId: produtoCamisetaManUtd.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoJaquetaManUtd.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBoneManUtd.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBermudaAdidas.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBermudaDcShoes.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoCamisetaDcShoes.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBoneAdidas.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoTenisAdidas.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoCamisaGreenBayPackers.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoBoneGreenBayPackers.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoCanecaManUtd.Id, usuarioId: usuarioId),
                new LogEvento(entidadeId: produtoMeiaAdidas.Id, usuarioId: usuarioId)
            );
        }
    }
}
