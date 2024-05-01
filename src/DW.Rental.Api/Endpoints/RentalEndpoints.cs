using DW.Rental.Domain.Exception;
using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Requests.Rental;
using DW.Rental.Shareable.Requests.Rental.Input;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DW.Rental.Api.Endpoints;

public static class RentalEndpoints
{
    public static void MapRentalAppEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/rental", async (IMediator mediator, ClaimsPrincipal user, [FromBody] CreateRentalInput request) =>
        {
            var result = await mediator.Send(
                new CreateRentalRequest(
                    request.DataInicio,
                    request.DataPrevistaEntrega,
                    request.PlanEnum,
                    Guid.Parse(user.Claims.First(x=>x.Type == "deliverymanId").Value),
                    Enum.Parse<CnhTypeEnum>(user.Claims.First(x => x.Type == "cnh_type").Value)));

            if (result.IsSuccess)
                return Results.Created($"api/v1/moto{result.Value.Id}", result.Value.Id);

            if (result.Exception is NotFoundException)
                return Results.NotFound(result.Exception.Message);

            return Results.StatusCode(500);
        })
            .WithName("CreateRental")
            .WithDisplayName("CreateRental")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Rental")
            .RequireAuthorization("Deliveryman");
    }
}
