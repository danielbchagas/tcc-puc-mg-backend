using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using ECommerce.Basket.Domain.Models;
using Xunit;

namespace ECommerce.Basket.Domain.Test
{
    public class UnitTest4
    {
        private CustomerBasket _basket;

        public UnitTest4()
        {
            var faker = new Faker<CustomerBasket>()
                .CustomInstantiator(set => new CustomerBasket(customerId: Guid.NewGuid()));

            _basket = faker.Generate();
        }

        [Fact]
        public void AddItemToBasket_BasketMustContains1ItemAndValue1000()
        {
            // Arrange
            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(id: Guid.NewGuid(), name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), customerBasketId: _basket.Id));

            var item = faker.Generate();
            var item2 = faker.Generate();

            // Act
            _basket.UpdatesItems(item);
            _basket.UpdatesItems(item2);

            _basket.RemoveItem(item2);

            // Assert
            Assert.True(_basket.Items.Count() == 1 && _basket.Value == 1000);
        }
    }
}
