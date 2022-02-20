using Bogus;
using ECommerce.Ordering.Domain.Models;
using System;
using Xunit;

namespace ECommerce.Ordering.Test
{
    public class UnitTest1
    {
        [Fact]
        public void CloseOrder_ValueMustBe2500()
        {
            // Arrange
            var orderFaker = new Faker<Order>().CustomInstantiator(set => 
                new Order(id: Guid.NewGuid(), 
                    fullName: set.Person.FirstName + set.Person.LastName, 
                    document: set.Random.String(), 
                    phone: set.Person.Phone, 
                    email: set.Person.Email, 
                    firstLine: set.Person.Address.Street,
                    secondLine: "",
                    city: set.Person.Address.City, 
                    state: set.Address.State(),
                    zipCode: set.Person.Address.ZipCode));

            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var itemFaker1 = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item2Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item3Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item4Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item5Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));

            var order = orderFaker.Generate();
            
            order.Items.Add(itemFaker1.Generate());
            order.Items.Add(item2Faker.Generate());
            order.Items.Add(item3Faker.Generate());
            order.Items.Add(item4Faker.Generate());
            order.Items.Add(item5Faker.Generate());

            // Act
            order.Totalize();

            // Assert
            Assert.True(order.Value == 2500);
        }

        [Fact]
        public void CloseOrder_ValueMustBe3000()
        {
            // Arrange
            var orderFaker = new Faker<Order>().CustomInstantiator(set =>
                new Order(id: Guid.NewGuid(), 
                    fullName: set.Person.FirstName + set.Person.LastName,
                    document: set.Random.String(),
                    phone: set.Person.Phone,
                    email: set.Person.Email,
                    firstLine: set.Person.Address.Street,
                    secondLine: "",
                    city: set.Person.Address.City,
                    state: set.Address.State(),
                    zipCode: set.Person.Address.ZipCode));

            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var itemFaker1 = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item2Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item3Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item4Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item5Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));
            var item6Faker = new Faker<OrderItem>().CustomInstantiator(set => 
                new OrderItem(id: Guid.NewGuid(), name: set.Random.String(), 5, 100, image: set.Image.PicsumUrl(), productId: productId, orderId: customerId));

            var order = orderFaker.Generate();

            order.Items.Add(itemFaker1.Generate());
            order.Items.Add(item2Faker.Generate());
            order.Items.Add(item3Faker.Generate());
            order.Items.Add(item4Faker.Generate());
            order.Items.Add(item5Faker.Generate());
            order.Items.Add(item6Faker.Generate());

            // Act
            order.Totalize();

            // Assert
            Assert.True(order.Value == 3000);
        }
    }
}
