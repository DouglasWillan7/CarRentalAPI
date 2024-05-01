using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Requests.Login;
using DW.Rental.Shareable.Requests.Motorcycle;
using DW.Rental.Shareable.Requests.Motorcycle.Input;
using DW.Rental.Shareable.Requests.Rental.Input;
using DW.Rental.Shareable.Responses.Login;
using DW.Rental.Shareable.Responses.Motorcycle;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Dw.Rental.Test.Integration;

public class RentalTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public RentalTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task Post_CreateRental_ReturnOk()
    {
        var token = await Login();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Arrange
        var request = new CreateRentalInput(DateTime.Now.AddDays(1), DateTime.Now.AddDays(7), "0147845", PlanEnum.semanal);

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/rental", request);

        var result = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }


    private async Task<string> Login()
    {
        // Arrange
        var request = new LoginRequest("douglas", "123456");

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/login", request);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync(), _options);

        return result!.Token;
    }
}
