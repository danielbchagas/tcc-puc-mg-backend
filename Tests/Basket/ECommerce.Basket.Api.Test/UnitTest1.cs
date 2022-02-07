using ECommerce.Basket.Api.Test.Factory;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ECommerce.Basket.Api.Test
{
    public class UnitTest1 : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UnitTest1(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange


            //Act
            var response = await _client.GetAsync($"api/CustomerBaskets/{Guid.NewGuid()}");

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
        }
    }
}
