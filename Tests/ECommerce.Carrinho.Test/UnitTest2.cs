using Bogus;
using ECommerce.Carrinho.Domain.Models;
using System;
using Xunit;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Cart;

namespace ECommerce.Carrinho.Test
{
    public class UnitTest2
    {
        private CarrinhoCliente Carrinho { get; }

        public UnitTest2()
        {
            var faker = new Faker<CarrinhoCliente>()
                .CustomInstantiator(set => new CarrinhoCliente(customerId: Guid.NewGuid()));

            Carrinho = faker.Generate();
        }

        [Fact]
        public void AdicionarItemCarrinho_ValorDoCarrinhoDeveSer1000()
        {
            // Arrange
            var faker = new Faker<Item>()
                .CustomInstantiator(set => new Item(name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), cartId: Carrinho.Id));

            var item = faker.Generate();

            // Act
            Carrinho.AddItens(item);

            // Assert
            Assert.Equal(1000, Carrinho.Value);
        }

        [Fact]
        public void DiminuirQuantidadeItemCarrinho_QuantidadeDeItensCarrinhoDeveSer3()
        {
            // Arrange
            var produtoId = Guid.NewGuid();

            var faker = new Faker<Item>()
                .CustomInstantiator(set => new Item(name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), cartId: Carrinho.Id));

            var primeiroItem = faker.Generate();

            var faker2 = new Faker<Item>()
                .CustomInstantiator(set => new Item(name: primeiroItem.Name, quantity: 3, value: primeiroItem.Value, image: primeiroItem.Image, productId: primeiroItem.ProductId, cartId: Carrinho.Id));

            var segundoItem = faker2.Generate();

            // Act
            Carrinho.AddItens(primeiroItem);
            Carrinho.AddItens(segundoItem);

            // Assert
            Assert.Contains(Carrinho.Itens, ic => ic.Quantity == 3);
        }

        [Fact]
        public void AumentarQuantidadeItensCarrinho_DeveRetornarErro()
        {
            // Arrange
            var produtoId = Guid.NewGuid();

            var faker = new Faker<Item>()
                .CustomInstantiator(set => new Item(name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), cartId: Carrinho.Id));

            var primeiraInteracao = faker.Generate();

            var faker2 = new Faker<Item>()
                .CustomInstantiator(set => new Item(name: primeiraInteracao.Name, quantity: 6, value: primeiraInteracao.Value, image: primeiraInteracao.Image, productId: primeiraInteracao.ProductId, cartId: Carrinho.Id));

            var segundaInteracao = faker2.Generate();

            // Act
            var validationResult = Carrinho.AddItens(primeiraInteracao);
            validationResult.Errors.AddRange(Carrinho.AddItens(segundaInteracao).Errors);

            // Assert
            Assert.Contains(validationResult.Errors, ic => ic.ErrorMessage.Contains($"A quantity mínima do {primeiraInteracao.Name} é 1 e o máxima do {primeiraInteracao.Name} é 5."));
        }
    }
}
