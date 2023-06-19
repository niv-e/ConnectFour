using Microsoft.AspNetCore.Mvc.Testing;
using Model.bounderies;
using System.Text;
using System.Text.Json;

namespace IntegrationTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task RegisterPlayer_HappyFlow()
        {
            // Arrange
            var client = _factory.CreateClient();
            var requestBody = new PlayerBoundary { 
                Id = 1,
                FirstName = "Niv",
                Country = "Israel",
                PhoneNumber = "+972-1234567"
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var registerUrl = client?.BaseAddress?.ToString() + "api/profile/register";

            // Act
            HttpResponseMessage response = await client?.PostAsync(registerUrl, content);

            // Assert
            response.EnsureSuccessStatusCode();

        }
    }
}