using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Requests.Deliveryman.Input;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Dw.Rental.Test.Integration;

public class DeliverymanTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public DeliverymanTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Post_CreateNewDeliveryman_ReturnCreated()
    {
        // Arrange
        var request = new CreateDeliverymanInput("Douglas Martins", "123456", "40.400.40", new DateTime(1997, 01, 09), 888888888, CnhTypeEnum.A);

        // Act
        var response = await _client.PostAsJsonAsync("api/v1/deliveryman", request);

        var result = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<string>(result);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}