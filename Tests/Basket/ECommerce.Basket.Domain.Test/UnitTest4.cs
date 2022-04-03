using Bogus;
using ECommerce.Basket.Domain.Models;
using System;
using System.Linq;
using Xunit;

namespace ECommerce.Basket.Domain.Test
{
    public class UnitTest4
    {
        private ShoppingBasket _basket;

        public UnitTest4()
        {
            var faker = new Faker<ShoppingBasket>()
                .CustomInstantiator(set => new ShoppingBasket(id: Guid.NewGuid(), customerId: Guid.NewGuid()));

            _basket = faker.Generate();
        }

        [Fact]
        public void AddItemToBasket_BasketMustContains1ItemAndValue1000()
        {
            // Arrange
            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(id: Guid.NewGuid(), name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), shoppingBasketId: _basket.Id));

            var item = faker.Generate();
            var item2 = faker.Generate();

            // Act
            _basket.UpdatesItems(item);
            _basket.UpdatesItems(item2);

            _basket.RemoveItem(item2);

            _basket.UpdateBasketValue();

            // Assert
            Assert.True(_basket.Items.Count() == 1 && _basket.Value == 1000);
        }
    }
}
