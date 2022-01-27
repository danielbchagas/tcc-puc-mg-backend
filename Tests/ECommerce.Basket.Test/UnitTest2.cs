using Bogus;
using ECommerce.Basket.Domain.Models;
using System;
using Xunit;

namespace ECommerce.Carrinho.Test
{
    public class UnitTest2
    {
        private CustomerBasket _basket { get; }

        public UnitTest2()
        {
            var faker = new Faker<CustomerBasket>()
                .CustomInstantiator(set => new CustomerBasket(customerId: Guid.NewGuid()));

            _basket = faker.Generate();
        }

        [Fact]
        public void AdicionarItemCarrinho_ValorDoCarrinhoDeveSer1000()
        {
            // Arrange
            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), customerBasketId: _basket.Id));

            var item = faker.Generate();

            // Act
            _basket.AddItens(item);

            // Assert
            Assert.Equal(1000, _basket.Value);
        }

        [Fact]
        public void DiminuirQuantidadeItemCarrinho_QuantidadeDeItensCarrinhoDeveSer3()
        {
            // Arrange
            var produtoId = Guid.NewGuid();

            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), customerBasketId: _basket.Id));

            var primeiroItem = faker.Generate();

            var faker2 = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(name: primeiroItem.Name, quantity: 3, value: primeiroItem.Value, image: primeiroItem.Image, productId: primeiroItem.ProductId, customerBasketId: _basket.Id));

            var segundoItem = faker2.Generate();

            // Act
            _basket.AddItens(primeiroItem);
            _basket.AddItens(segundoItem);

            // Assert
            Assert.Contains(_basket.Itens, ic => ic.Quantity == 3);
        }

        [Fact]
        public void AumentarQuantidadeItensCarrinho_DeveRetornarErro()
        {
            // Arrange
            var produtoId = Guid.NewGuid();

            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), customerBasketId: _basket.Id));

            var primeiraInteracao = faker.Generate();

            var faker2 = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(name: primeiraInteracao.Name, quantity: 6, value: primeiraInteracao.Value, image: primeiraInteracao.Image, productId: primeiraInteracao.ProductId, customerBasketId: _basket.Id));

            var segundaInteracao = faker2.Generate();

            // Act
            var validationResult = _basket.AddItens(primeiraInteracao);
            validationResult.Errors.AddRange(_basket.AddItens(segundaInteracao).Errors);

            // Assert
            Assert.Contains(validationResult.Errors, ic => ic.ErrorMessage.Contains($"A quantity mínima do {primeiraInteracao.Name} é 1 e o máxima do {primeiraInteracao.Name} é 5."));
        }
    }
}
