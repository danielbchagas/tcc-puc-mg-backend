using System;
using Bogus;
using ECommerce.Basket.Domain.Models;
using Xunit;

namespace ECommerce.Basket.Domain.Test
{
    public class UnitTest2
    {
        private ShoppingBasket _basket { get; }

        public UnitTest2()
        {
            var faker = new Faker<ShoppingBasket>()
                .CustomInstantiator(set => new ShoppingBasket(id: Guid.NewGuid(), customerId: Guid.NewGuid()));

            _basket = faker.Generate();
        }

        [Fact]
        public void AddItemToBasket_ValueMustBe1000()
        {
            // Arrange
            var faker = new Faker<BasketItem>()
                .CustomInstantiator(set => new BasketItem(id: Guid.NewGuid(), name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), shoppingBasketId: _basket.Id));

            var item = faker.Generate();

            // Act
            _basket.UpdatesItems(item);

            _basket.UpdateBasketValue();

            // Assert
            Assert.Equal(1000, _basket.Value);
        }

        [Fact]
        public void AddItemToBasket_MustThrowError()
        {
            // Arrange
            var fakerItem = new Faker<BasketItem>()
                .CustomInstantiator(set => 
                    new BasketItem(id: Guid.NewGuid(), name: set.Random.String(), quantity: 5, value: 200, image: set.Image.PicsumUrl(), productId: Guid.NewGuid(), shoppingBasketId: _basket.Id));

            var firstItem = fakerItem.Generate();

            var faker2 = new Faker<BasketItem>()
                .CustomInstantiator(set => 
                    new BasketItem(id: firstItem.Id, name: firstItem.Name, quantity: 6, value: firstItem.Value, image: firstItem.Image, productId: Guid.NewGuid(), shoppingBasketId: _basket.Id));

            var secondItem = faker2.Generate();

            // Act
            var validationResult = _basket.UpdatesItems(firstItem);
            validationResult.Errors.AddRange(_basket.UpdatesItems(secondItem).Errors);

            // Assert
            Assert.Contains(validationResult.Errors, ic => ic.ErrorMessage.Contains($"A quantity mínima do {firstItem.Name} é 1 e o máxima do {firstItem.Name} é 5."));
        }
    }
}
