using AutoFixture;
using System.Linq;
using Xunit;

namespace ECommerce.Baskets.Domain.Test
{
    public class RemoveItemsFromBasket
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void RemoveItems()
        {
            // Arrange
            var basket = _fixture.Build<Basket.Domain.Models.Basket>()
                .Without(b => b.Items)
                .Create();
            var items = _fixture.Build<Basket.Domain.Models.Item>()
                .CreateMany();

            // Act
            // 1 - Update basket [Including new items]
            basket.UpdatesItems(items);
            // 2 - Select the item to remove
            var selected = items.First();
            // 3 - Removes item from the basket
            basket.Items.Remove(selected);
            // 4 - Update basket [With the first product of the list removed]
            basket.UpdatesItems(basket.Items);

            // Asset
            Assert.False(basket.Items.Contains(selected));
        }
    }
}
