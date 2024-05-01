using DW.Rental.Shareable.Requests.Login;
using DW.Rental.Shareable.Requests.Motorcycle.Input;
using DW.Rental.Shareable.Responses.Login;
using DW.Rental.Shareable.Responses.Motorcycle;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;

namespace Dw.Rental.Test.Integration;

public class MotorcycleTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public MotorcycleTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task Post_CreateNewMotorcycle_ReturnErrorUnauthorized()
    {
        // Arrange
        var request = new CreateMotorcycleInput(2004, "Titan 160", "PXY-89A5");

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/motorcycle", request);

        var result = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Post_CreateNewMotorcycle_ReturnCreted()
    {
        // Arrange
        var request = new CreateMotorcycleInput(2004, "Titan 160", "PXY-89A5");
        var token = await Login();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        // Act

        var response = await _client.PostAsJsonAsync("api/v1/motorcycle", request);

        var result = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }


    [Fact]
    public async Task Get_NewMotorcycle_ReturnCreted()
    {
        // Arrange
        var request = new CreateMotorcycleInput(2004, "Titan 160", "PXY-89B5");
        var token = await Login();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/motorcycle", request);

        var result = JsonSerializer.Deserialize<CreateMotorcycleResponse>(await response.Content.ReadAsStringAsync(), _options);

        var getResponse = await _client.GetAsync($"api/v1/motorcycle/{result.LicensePlate}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(request.LicensePlate, result.LicensePlate);
    }

    [Fact]
    public async Task Update_NewMotorcycle_ReturnOk()
    {
        // Arrange
        var request = new CreateMotorcycleInput(2004, "Titan 160", "PXY-89F5");
        var token = await Login();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var newLicensePlate = new Faker().Vehicle.Vin();

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/motorcycle", request);

        var result = JsonSerializer.Deserialize<CreateMotorcycleResponse>(await response.Content.ReadAsStringAsync(), _options);

        var updateResponse = await _client.PutAsJsonAsync($"api/v1/motorcycle/{result.LicensePlate}", newLicensePlate);

        var resultUpdateMotorcycle = JsonSerializer.Deserialize<UpdateMotorcycleResponse>(await updateResponse.Content.ReadAsStringAsync(), _options);

        // Assert
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        Assert.Equal(newLicensePlate, resultUpdateMotorcycle!.LicensePlate);
    }

    [Fact]
    public async Task Delete_NewMotorcycle_ReturnOk()
    {
        // Arrange
        var request = new CreateMotorcycleInput(2004, "Titan 160", "PXY-89Y5");
        var token = await Login();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/motorcycle", request);

        var result = JsonSerializer.Deserialize<CreateMotorcycleResponse>(await response.Content.ReadAsStringAsync(), _options);

        var deleteResponse = await _client.DeleteAsync($"api/v1/motorcycle/{result.LicensePlate}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
    }


    private async Task<string> Login()
    {
        // Arrange
        var request = new LoginRequest("admin", "admin");

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
