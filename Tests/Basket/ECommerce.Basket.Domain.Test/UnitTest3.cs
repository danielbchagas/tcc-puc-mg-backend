using Bogus;
using ECommerce.Core.Models.Basket;
using System;
using Xunit;

namespace ECommerce.Basket.Domain.Test
{
    public class UnitTest3
    {
        private CustomerBasket _basket { get; }

        public UnitTest3()
        {
            var faker = new Faker<CustomerBasket>()
                .CustomInstantiator(set => new CustomerBasket(customerId: Guid.NewGuid()));

            _basket = faker.Generate();
        }

        [Fact]
        public void RemoveItem_ValueMustBe200()
        {
            // Arrange
            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), customerBasketId: _basket.Id));

            var item = faker.Generate();
            
            // Act
            _basket.UpdatesItems(item);

            item.Quantity = 1;

            _basket.UpdatesItems(item);

            // Assert
            Assert.True(_basket.Value == 200);
        }
    }
}
