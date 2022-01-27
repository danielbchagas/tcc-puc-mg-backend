using Bogus;
using ECommerce.Carrinho.Domain.Models;
using System;
using Xunit;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Cart;

namespace ECommerce.Carrinho.Test
{
    public class UnitTest1
    {
        private CarrinhoCliente Carrinho { get; }

        public UnitTest1()
        {
            var faker = new Faker<CarrinhoCliente>()
                .CustomInstantiator(set => new CarrinhoCliente(customerId: Guid.NewGuid()));

            Carrinho = faker.Generate();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        public void AdicionarItemCarrinho_DeveLancarErro(int quantidade)
        {
            // Arrange
            var faker = new Faker<Item>()
                .CustomInstantiator(set => new Item(name: set.Random.String(), quantity: quantidade, value: set.Random.Decimal(), image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), cartId: Carrinho.Id));

            var item = faker.Generate();

            // Act
            var result = Carrinho.AddItens(item);

            // Assert
            Assert.False(result.IsValid);
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
            var faker = new Faker<Item>()
                .CustomInstantiator(set => new Item(name: set.Random.String(), quantity: quantidade, value: set.Random.Decimal(), image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), cartId: Carrinho.Id));

            var item = faker.Generate();

            // Act
            var result = Carrinho.AddItens(item);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
