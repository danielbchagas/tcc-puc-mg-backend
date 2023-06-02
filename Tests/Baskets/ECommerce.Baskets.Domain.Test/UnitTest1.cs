using System;
using Bogus;
using Xunit;

namespace ECommerce.Baskets.Domain.Test
{
    public class UnitTest1
    {
        private Basket.Domain.Models.Basket _basket;

        public UnitTest1()
        {
            var faker = new Faker<Basket.Domain.Models.Basket>()
                .CustomInstantiator(set => new Basket.Domain.Models.Basket(id: Guid.NewGuid(), customerId: Guid.NewGuid()));

            _basket = faker.Generate();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        public void AddItemToBasket_ThrowError(int quantity)
        {
            // Arrange
            var faker = new Faker<Basket.Domain.Models.Item>()
                .CustomInstantiator(set => new Basket.Domain.Models.Item(
                    id: Guid.NewGuid(),
                    name: set.Random.String(),
                    quantity: quantity,
                    value: set.Random.Decimal(),
                    image: set.Image.PicsumUrl(),
                    productId: Guid.NewGuid(),
                    basketId: _basket.Id));

            var item = faker.Generate();

            // Act
            var result = _basket.UpdatesItems(item);

            // Assert
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void AddItemToBasket_Success(int quantity)
        {
            // Arrange
            var faker = new Faker<Basket.Domain.Models.Item>()
                .CustomInstantiator(set => new Basket.Domain.Models.Item(
                    id: Guid.NewGuid(), name: set.Random.String(),
                    quantity: quantity, value: set.Random.Decimal(),
                    image: set.Image.PicsumUrl(),
                    productId: Guid.NewGuid(),
                    basketId: _basket.Id));

            var item = faker.Generate();

            // Act
            var result = _basket.UpdatesItems(item);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
