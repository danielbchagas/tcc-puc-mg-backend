using ECommerce.Catalogo.Domain.Interfaces.Data;
using ECommerce.Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Resources;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Produto> Produtos { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
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
                p.Property(p => p.Quantidade).HasColumnType("int").IsRequired();
                p.Property(p => p.Imagem).HasColumnType("text").IsRequired(false);
                p.Property(p => p.Ativo).HasColumnType("bit").IsRequired();
                p.Property(p => p.Valor).HasColumnType("money").IsRequired();
                p.Property(p => p.DataCadastro).HasColumnType("datetime").IsRequired();
            });
            #endregion

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            var rm = new ResourceManager(typeof(ImagensResource));

            var produtoCamisetaManUtd = new Produto(
                descricao: "Manchester United Football Club",
                nome: "Camiseta",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camisa_man_utd")),
                quantidade: 100,
                valor: 200.50m
            );
            var produtoJaquetaManUtd = new Produto(
                descricao: "Manchester United Football Club",
                nome: "Jaqueta",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_man_utd")),
                quantidade: 250,
                valor: 300.50m
            );
            var produtoBoneManUtd = new Produto(
                descricao: "Manchester United Football Club",
                nome: "Boné",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bone_man_utd")),
                quantidade: 10,
                valor: 80.50m
            );
            var produtoBermudaAdidas = new Produto(
                descricao: "Adidas",
                nome: "Bermuda",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bermuda_adidas")),
                quantidade: 10,
                valor: 150.50m
            );
            var produtoBermudaDcShoes = new Produto(
                descricao: "Dc Shoes",
                nome: "Bermuda",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bermuda_dc_shoes")),
                quantidade: 10,
                valor: 150.50m
            );
            var produtoCamisetaDcShoes = new Produto(
                descricao: "Dc Shoes",
                nome: "Camiseta",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes")),
                quantidade: 10,
                valor: 75.50m
            );
            var produtoBoneAdidas = new Produto(
                descricao: "Adidas",
                nome: "Bone",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bone_adidas")),
                quantidade: 10,
                valor: 120.50m
            );
            var produtoTenisAdidas = new Produto(
                descricao: "Adidas",
                nome: "Tenis",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("tenis_adidas")),
                quantidade: 10,
                valor: 350.50m
            );
            var produtoCamisaGreenBayPackers = new Produto(
                descricao: "Green Bay Packers",
                nome: "Camisa",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camisa_green_bay_packers")),
                quantidade: 10,
                valor: 400.50m
            );
            var produtoBoneGreenBayPackers = new Produto(
                descricao: "Green Bay Packers",
                nome: "Boné",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bone_green_bay_packers")),
                quantidade: 10,
                valor: 150.50m
            );
            var produtoCanecaManUtd = new Produto(
                descricao: "Manchester United Football Club",
                nome: "Caneca",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("caneca_man_utd")),
                quantidade: 10,
                valor: 30.50m
            );
            var produtoMeiaAdidas = new Produto(
                descricao: "Adidas",
                nome: "Meia",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("meia_adidas")),
                quantidade: 10,
                valor: 20.50m
            );
            var produtoBermudaDcShoes2 = new Produto(
                descricao: "DC Shoes",
                nome: "Bermuda",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bermuda_dc_shoes_2")),
                quantidade: 15,
                valor: 200.50m
            );
            var produtoBoneDcShoes = new Produto(
                descricao: "DC Shoes",
                nome: "Boné",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("bone_dc_shoes")),
                quantidade: 10,
                valor: 150.50m
            );
            var produtoCamisetaDcShoes2 = new Produto(
                descricao: "DC Shoes",
                nome: "Camiseta",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes_2")),
                quantidade: 10,
                valor: 80.00m
            );
            var produtoCamisetaDcShoes3 = new Produto(
                descricao: "Adidas",
                nome: "Camiseta",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("camiseta_dc_shoes_3")),
                quantidade: 10,
                valor: 90.50m
            );
            var produtoJaquetaDcShoes = new Produto(
                descricao: "DC Shoes",
                nome: "Jaqueta",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_dc_shoes")),
                quantidade: 10,
                valor: 200.00m
            );
            var produtoMeiaDcShoes = new Produto(
                descricao: "DC Shoes",
                nome: "Meia",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("meia_dc_shoes")),
                quantidade: 10,
                valor: 20.50m
            );
            var produtoTenisDcShoes = new Produto(
                descricao: "DC Shoes",
                nome: "Tênis",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("tenis_dc_shoes")),
                quantidade: 10,
                valor: 250.50m
            );
            var produtoJaquetaGreenBayPackers = new Produto(
                descricao: "Adidas",
                nome: "Meia",
                imagem: Convert.ToBase64String((byte[])rm.GetObject("jaqueta_green_bay_packers")),
                quantidade: 10,
                valor: 20.50m
            );

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
                produtoMeiaAdidas,
                produtoBermudaDcShoes2,
                produtoBoneDcShoes,
                produtoCamisetaDcShoes2,
                produtoCamisetaDcShoes3,
                produtoJaquetaDcShoes,
                produtoMeiaDcShoes,
                produtoTenisDcShoes,
                produtoJaquetaGreenBayPackers
            );
        }
    }
}
