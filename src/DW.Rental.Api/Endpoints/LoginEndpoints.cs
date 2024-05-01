using DW.Rental.Shareable.Requests.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DW.Rental.Api.Endpoints;

public static class LoginEndpoints
{
    public static void MapLoginAppEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/login", async (IMediator mediator, [FromBody] LoginRequest loginRequest) =>
        {
            var result = await mediator.Send(loginRequest);

            if (result.IsSuccess)
                return Results.Ok(result.Value);

            return Results.NotFound(result.Exception.Message);
        })
            .WithName("Login")
            .WithDisplayName("Login")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Login");
    }
}
