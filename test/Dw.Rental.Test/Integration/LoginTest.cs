using DW.Rental.Shareable.Requests.Login;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Dw.Rental.Test.Integration;

public class LoginTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public LoginTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Post_Login_ReturnToken()
    {
        // Arrange
        var request = new LoginRequest("admin", "admin");

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/login", request);

        var result = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<string>(result);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}