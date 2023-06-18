using Bogus;
using Bogus.Extensions.Brazil;
using ECommerce.Identity.Api.Models.Request;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace ECommerce.Identity.Api.Test
{
    public class Account : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Account(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateUser_MustCreate()
        {
            // Arrange
            var http = _factory.CreateClient();
            http.BaseAddress = new Uri("https://localhost:5001");

            var user = new Faker<SignUpUserRequest>()
                .RuleFor(u => u.FirstName, u => u.Person.FirstName)
                .RuleFor(u => u.LastName, u => u.Person.LastName)
                .RuleFor(u => u.Document, u => u.Person.Cpf())
                .RuleFor(u => u.Phone, u => u.Phone.PhoneNumber())
                .RuleFor(u => u.Email, u => u.Internet.Email())
                .RuleFor(u => u.Password, "Test@123")
                .RuleFor(u => u.PasswordConfirmation, "Test@123")
                .RuleFor(u => u.ZipCode, "01001000")
                .Generate();

            // Act
            var result = await http.PostAsJsonAsync("/api/Account/sign-up", user);

            // Assert
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}