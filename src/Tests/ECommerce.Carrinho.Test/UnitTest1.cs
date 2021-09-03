using Bogus;
using System;
using Xunit;
namespace ECommerce.Carrinho.Test
{
    public class UnitTest1
    {
        private Api.Models.Carrinho Carrinho { get; }

        public UnitTest1()
        {
            var faker = new Faker<Api.Models.Carrinho>()
                .CustomInstantiator(set => new Api.Models.Carrinho(clienteId: Guid.NewGuid()));

            Carrinho = faker.Generate();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        public void AdicionarItemCarrinho_DeveLancarErro(int quantidade)
        {
            // Arrange
            var faker = new Faker<Api.Models.ItemCarrinho>()
                .CustomInstantiator(set => new Api.Models.ItemCarrinho(nome: set.Random.String(), quantidade: quantidade, valor: set.Random.Decimal(), imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid(), carrinhoId: Carrinho.Id));

            var item = faker.Generate();

            // Act
            Carrinho.AtualizarItem(item);

            // Assert
            Assert.False(Carrinho.Validacao.IsValid);
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
            var faker = new Faker<Api.Models.ItemCarrinho>()
                .CustomInstantiator(set => new Api.Models.ItemCarrinho(nome: set.Random.String(), quantidade: quantidade, valor: set.Random.Decimal(), imagem: set.Image.PicsumUrl(), produtoId: Guid.NewGuid(), carrinhoId: Carrinho.Id));

            var item = faker.Generate();

            // Act
            Carrinho.AtualizarItem(item);

            // Assert
            Assert.True(Carrinho.Validacao.IsValid);
        }
    }
}
