using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Resources;
using System.Threading.Tasks;

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

                p.Property(p => p.Descricao).HasColumnType("varchar(50)").IsRequired();
                p.Property(p => p.Nome).HasColumnType("varchar(50)").IsRequired();
                p.Property(p => p.QuantidadeEstoque).HasColumnType("int").IsRequired();
                p.Property(p => p.Imagem).HasColumnType("text").IsRequired(false);
                p.Property(p => p.Ativo).HasColumnType("bit").IsRequired();
                p.Property(p => p.Preco).HasColumnType("money").IsRequired();
                p.Property(p => p.DataCadastro).HasColumnType("datetime").IsRequired();
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
                descricao: "Manchester United Football Club",
                nome: "Camiseta",
                imagem: imagemCamisaManUtd,
                quantidadeEstoque: 100,
                preco: 200.50m
            );
            var produtoJaquetaManUtd = new Produto(
                descricao: "Manchester United Football Club",
                nome: "Jaqueta",
                imagem: imagemJaquetaManUtd,
                quantidadeEstoque: 250,
                preco: 300.50m
            );
            var produtoBoneManUtd = new Produto(
                descricao: "Manchester United Football Club",
                nome: "Boné",
                imagem: imagemBoneManUtd,
                quantidadeEstoque: 10,
                preco: 80.50m
            );
            var produtoBermudaAdidas = new Produto(
                descricao: "Adidas",
                nome: "Bermuda",
                imagem: imagemBermudaAdidas,
                quantidadeEstoque: 10,
                preco: 150.50m
            );
            var produtoBermudaDcShoes = new Produto(
                descricao: "Dc Shoes",
                nome: "Bermuda",
                imagem: imagemBermudaDcShoes,
                quantidadeEstoque: 10,
                preco: 150.50m
            );
            var produtoCamisetaDcShoes = new Produto(
                descricao: "Dc Shoes",
                nome: "Camiseta",
                imagem: imagemCamisetaDcShoes,
                quantidadeEstoque: 10,
                preco: 100.50m
            );
            var produtoBoneAdidas = new Produto(
                descricao: "Adidas",
                nome: "Bone",
                imagem: imagemBoneAdidas,
                quantidadeEstoque: 10,
                preco: 60.50m
            );
            var produtoTenisAdidas = new Produto(
                descricao: "Adidas",
                nome: "Tenis",
                imagem: imagemTenisAdidas,
                quantidadeEstoque: 10,
                preco: 350.50m
            );
            var produtoCamisaGreenBayPackers = new Produto(
                descricao: "Green Bay Packers",
                nome: "Camisa",
                imagem: imagemCamisaGreenBayPackers,
                quantidadeEstoque: 10,
                preco: 400.50m
            );
            var produtoBoneGreenBayPackers = new Produto(
                descricao: "Green Bay Packers",
                nome: "Bone",
                imagem: imagemBoneGreenBayPackers,
                quantidadeEstoque: 10,
                preco: 150.50m
            );
            var produtoCanecaManUtd = new Produto(
                descricao: "Manchester United Football Club",
                nome: "Caneca",
                imagem: imagemCanecaManUtd,
                quantidadeEstoque: 10,
                preco: 30.50m
            );
            var produtoMeiaAdidas = new Produto(
                descricao: "Adidas",
                nome: "Meia",
                imagem: imagemMeiaAdidas,
                quantidadeEstoque: 10,
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
        }
    }
}
