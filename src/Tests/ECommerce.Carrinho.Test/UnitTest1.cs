using Bogus;
using ECommerce.Carrinho.Domain.Models;
using System;
using Xunit;

namespace ECommerce.Carrinho.Test
{
    public class UnitTest1
    {
        private Domain.Models.Carrinho Carrinho { get; }

        public UnitTest1()
        {
            var faker = new Faker<Domain.Models.Carrinho>()
                .CustomInstantiator(set => new Domain.Models.Carrinho(clienteId: Guid.NewGuid()));

            Carrinho = faker.Generate();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        public void AdicionarItemCarrinho_DeveLancarErro(int quantidade)
        {
            // Arrange
            var faker = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: set.Random.String(), quantidade: quantidade, valor: set.Random.Decimal(), imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid(), carrinhoId: Carrinho.Id));

            var item = faker.Generate();

            // Act
            Carrinho.AtualizarItem(item);

            // Assert
            Assert.False(Carrinho.Validar().IsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void AdicionarItemCarrinho_DeveAdicionarComSucesso(int quantidade)
        {
            // Arrange
            var faker = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: set.Random.String(), quantidade: quantidade, valor: set.Random.Decimal(), imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid(), carrinhoId: Carrinho.Id));

            var item = faker.Generate();

            // Act
            Carrinho.AtualizarItem(item);

            // Assert
            Assert.True(Carrinho.Validar().IsValid);
        }

        [Fact]
        public void AdicionarItemCarrinho_ValorDoCarrinhoDeveSer1000()
        {
            // Arrange
            var faker = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: set.Random.String(), quantidade: 5, valor: 200, imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid(), carrinhoId: Carrinho.Id));

            var item = faker.Generate();

            // Act
            Carrinho.AtualizarItem(item);

            // Assert
            Assert.Equal(1000, Carrinho.ValorTotal);
        }

        [Fact]
        public void DiminuirQuantidadeItemCarrinho_QuantidadeDeItensCarrinhoDeveSer3()
        {
            // Arrange
            var produtoId = Guid.NewGuid();

            var faker = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: set.Random.String(), quantidade: 5, valor: 200, imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid(), carrinhoId: Carrinho.Id));

            var primeiroItem = faker.Generate();

            var faker2 = new Faker<ItemCarrinho>()
                .CustomInstantiator(set => new ItemCarrinho(nome: primeiroItem.Nome, quantidade: 3, valor: primeiroItem.Valor, imagem: primeiroItem.Imagem, produtoId: primeiroItem.ProdutoId, carrinhoId: Carrinho.Id));

            var segundoItem = faker2.Generate();

            // Act
            Carrinho.AtualizarItem(primeiroItem);
            Carrinho.AtualizarItem(segundoItem);

            // Assert
            Assert.Contains(Carrinho.Itens, ic => ic.Quantidade == 3);
        }
    }
}
