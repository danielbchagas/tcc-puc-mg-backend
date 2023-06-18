using ECommerce.Identity.Api.Models.Request;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace ECommerce.Identity.Api.Test
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var http = _factory.CreateClient();
            http.BaseAddress = new Uri("https://localhost:5001");

            // Act
            var result = await http.PostAsJsonAsync("/api/Account/sign-up", new SignUpUserRequest
            {
                Id = Guid.NewGuid(),
                FirstName = "AAA",
                LastName = "BBB",
                Email = "integration_test@email.com",
                Phone = "(27) 12345-6789",
                Document = "111.222.333-44",
                Password = "Admin@123",
                PasswordConfirmation = "Admin@123"
            });

            // Assert
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}