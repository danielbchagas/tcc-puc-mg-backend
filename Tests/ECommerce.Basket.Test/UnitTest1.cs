using Bogus;
using ECommerce.Basket.Domain.Models;
using System;
using Xunit;

namespace ECommerce.Carrinho.Test
{
    public class UnitTest1
    {
        private CustomerBasket _basket;

        public UnitTest1()
        {
            var faker = new Faker<CustomerBasket>()
                .CustomInstantiator(set => new CustomerBasket(customerId: Guid.NewGuid()));

            _basket = faker.Generate();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        public void AddItemToBasket_ThrowError(int quantidade)
        {
            // Arrange
            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(name: set.Random.String(), quantity: quantidade, value: set.Random.Decimal(), image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), customerBasketId: _basket.Id));

            var item = faker.Generate();

            // Act
            var result = _basket.AddItens(item);

            // Assert
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void AddItemToBasket_Success(int quantidade)
        {
            // Arrange
            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(name: set.Random.String(), quantity: quantidade, value: set.Random.Decimal(), image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), customerBasketId: _basket.Id));

            var item = faker.Generate();

            // Act
            var result = _basket.AddItens(item);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
