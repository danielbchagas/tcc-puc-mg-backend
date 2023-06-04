using AutoFixture;
using ECommerce.Baskets.Domain.Models;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ECommerce.Baskets.Domain.Test.Models
{
    public class AddItemsIntoBasket
    {
        private readonly Fixture _fixture = new Fixture();

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1000)]
        public void AddOrUpdateItem_AddItemToBasket_ThrowError(int quantity)
        {
            // Arrange
            var basket = _fixture.Build<Basket>()
                .Create();
            
            var newItemsToAdd = _fixture.Build<Item>()
               .With(i => i.Quantity, quantity)
               .CreateMany();

            // Act
            var result = new List<ValidationResult>();
            
            foreach (var item in newItemsToAdd)
                result.Add(basket.AddOrUpdateItem(item));

            // Assert
            Assert.DoesNotContain(result, v => v.IsValid);
        }

        [Fact]
        public void AddOrUpdateItem_BasketWithoutItems_AddItemToBasket_Success()
        {
            // Arrange
            var basket = _fixture.Build<Basket>()
                .Without(b => b.Items)
                .Create();
            
            var newItemsToAdd = _fixture.Build<Item>()
                .With(i => i.Quantity, 1)
                .CreateMany();

            // Act
            foreach (var item in newItemsToAdd)
                basket.AddOrUpdateItem(item);

            // Assert
            Assert.True(basket.Items.Count() > 0);
        }

        [Fact]
        public void AddOrUpdateItem_RemoveItems_Success()
        {
            // Arrange
            var basket = _fixture.Build<Basket>()
                .Without(b => b.Items)
                .Create();
            
            var newItemsToAdd = _fixture.Build<Item>()
                .With(i => i.Quantity, 1)
                .Without(i => i.UpdatedAt)
                .Without(i => i.DeletedAt)
                .CreateMany();
            
            var selectedToRemove = newItemsToAdd.First();

            // Act
            // 1 - Update basket [Including new items]
            foreach (var item in newItemsToAdd)
                basket.AddOrUpdateItem(item);
            // 2 - Update basket [With the first product of the list removed]
            basket.RemoveItems(selectedToRemove);

            // Asset
            Assert.True(basket.Items.Count(i => i.DeletedAt != null) == 1);
        }

        [Fact]
        public void AddOrUpdateItem_RemoveItems_AddOrUpdateItem_Success()
        {
            // Arrange
            var basket = _fixture.Build<Basket>()
                .Without(b => b.Items)
                .Create();
            
            var newItemsToAdd = _fixture.Build<Item>()
                .With(i => i.Quantity, 1)
                .Without(i => i.UpdatedAt)
                .Without(i => i.DeletedAt)
                .CreateMany();
            
            var selectedToRemove = newItemsToAdd.First();
            
            var oneMoreToAdd = _fixture.Build<Item>()
                .With(i => i.Quantity, 1)
                .Without(i => i.UpdatedAt)
                .Without(i => i.DeletedAt)
                .Create();
            
            // Act
            // 1 - Update basket [Including new items]
            foreach (var item in newItemsToAdd)
                basket.AddOrUpdateItem(item);
            // 2 - Update basket [With the first product of the list removed]
            basket.RemoveItems(selectedToRemove);
            // 3 - Add the item again
            basket.AddOrUpdateItem(oneMoreToAdd);

            // Asset
            Assert.True(newItemsToAdd.Count(i => i.DeletedAt != null) == 1);
            Assert.True(basket.Items.Sum(s => s.Value) == (newItemsToAdd.Sum(s => s.Value) + oneMoreToAdd.Value));
        }
    }
}
