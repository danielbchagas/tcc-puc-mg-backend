using Bogus;
using ECommerce.Carrinho.Domain.Models;
using System;
using Xunit;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Carrinho;

namespace ECommerce.Carrinho.Test
{
    public class UnitTest1
    {
        private CarrinhoCliente Carrinho { get; }

        public UnitTest1()
        {
            var faker = new Faker<CarrinhoCliente>()
                .CustomInstantiator(set => new CarrinhoCliente(clienteId: Guid.NewGuid()));

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
            Carrinho.AdicionarItemAoCarrinho(item);

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
            Carrinho.AdicionarItemAoCarrinho(item);

            // Assert
            Assert.True(Carrinho.Validar().IsValid);
        }
    }
}
