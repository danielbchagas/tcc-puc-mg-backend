using AutoFixture;
using Xunit;

namespace ECommerce.Baskets.Domain.Test
{
    public class AddItemsIntoBasket
    {
        private readonly Fixture _fixture = new Fixture();

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void AddItemToBasket_ThrowError(int quantity)
        {
            // Arrange
            var basket = _fixture.Build<Basket.Domain.Models.Basket>()
                .Create();
            var items = _fixture.Build<Basket.Domain.Models.Item>()
               .With(i => i.Quantity, quantity)
               .CreateMany();

            // Act
            var result = basket.UpdatesItems(items);

            // Assert
            Assert.DoesNotContain(result, v => v.IsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void AddItemToBasket_Success(int quantity)
        {
            // Arrange
            var basket = _fixture.Build<Basket.Domain.Models.Basket>()
                .Without(b => b.Items)
                .Create();
            var items = _fixture.Build<Basket.Domain.Models.Item>()
                .With(i => i.Quantity, quantity)
                .CreateMany();

            // Act
            var result = basket.UpdatesItems(items);

            // Assert
            Assert.Contains(result, v => v.IsValid);
        }
    }
}
