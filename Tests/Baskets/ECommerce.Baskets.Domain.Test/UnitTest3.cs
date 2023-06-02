using Bogus;
using ECommerce.Basket.Domain.Models;
using System;
using Xunit;

namespace ECommerce.Baskets.Domain.Test
{
    public class UnitTest3
    {
        private Basket.Domain.Models.Basket _basket { get; }

        public UnitTest3()
        {
            var faker = new Faker<Basket.Domain.Models.Basket>()
                .CustomInstantiator(set => new Basket.Domain.Models.Basket(Guid.NewGuid(), customerId: Guid.NewGuid()));

            _basket = faker.Generate();
        }

        [Fact]
        public void RemoveItem_ValueMustBe200()
        {
            // Arrange
            var faker = new Faker<Item>()
                .CustomInstantiator(set => new Item(
                    id: Guid.NewGuid(),
                    name: set.Random.String(),
                    quantity: 5, value: 200,
                    image: set.Image.PicsumUrl(),
                    productId: Guid.NewGuid(),
                    basketId: _basket.Id));

            var item = faker.Generate();

            // Act
            _basket.UpdatesItems(item);

            item.Quantity = 1;

            _basket.UpdatesItems(item);

            _basket.UpdateBasketValue();

            // Assert
            Assert.True(_basket.Value == 200);
        }
    }
}
