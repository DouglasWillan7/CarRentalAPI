using DW.Rental.Domain.Exception;
using DW.Rental.Shareable.Requests.Motorcycle;
using DW.Rental.Shareable.Requests.Motorcycle.Input;
using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DW.Rental.Api.Endpoints;

public static class MotorcycleEndpoints
{
    public static void MapMotoAppEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("api/v1/motorcycle", async (IMediator mediator, [FromBody] CreateMotorcycleInput createMotorcycleInput) =>
            {
                var result = await mediator.Send(new CreateMotorcycleRequest(createMotorcycleInput.Year, createMotorcycleInput.Model, createMotorcycleInput.LicensePlate));

                if (result.IsSuccess)
                    return Results.Created($"api/v1/moto{result.Value.Id}", result.Value);

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            })
            .WithName("CreateMotorcycle")
            .WithDisplayName("CreateMotorcycle")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Motorcycle")
            .RequireAuthorization("Admin");

        app.MapGet("api/v1/motorcycle/{licensePlate}", async (IMediator mediator, string licensePlate) =>
        {
            var result = await mediator.Send(new GetMotorcycleRequest(licensePlate));

            if (result.IsSuccess)
                return Results.Ok(result.Value);

            return Results.NotFound();
        })
           .WithName("GetMotorcycle")
           .WithDisplayName("GetMotorcycle")
           .Produces<GetMotorcycleResponse>(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status404NotFound)
           .WithTags("Motorcycle")
           .RequireAuthorization("Admin");

        app.MapPut("api/v1/motorcycle/{licensePlate}", async (IMediator mediator, string licensePlate, [FromBody] string newLicensePlate) =>
        {
            var result = await mediator.Send(new UpdateMotorcycleRequest(licensePlate, newLicensePlate));

            if (result.IsSuccess)
                return Results.Ok(result.Value);

            if (result.Exception is NotFoundException)
                return Results.NotFound(result.Exception.Message);

            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        })
           .WithName("UpdateMotorcycle")
           .WithDisplayName("UpdateMotorcycle")
           .Produces<UpdateMotorcycleResponse>(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status404NotFound)
           .Produces(StatusCodes.Status500InternalServerError)
           .WithTags("Motorcycle")
           .RequireAuthorization("Admin");


        app.MapDelete("api/v1/motorcycle/{placa}", async (IMediator mediator, string placa) =>
        {
            var result = await mediator.Send(new DeleteMotorcycleRequest(placa));

            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }

            return Results.BadRequest("Erro");
        })
           .WithName("DeleteMotorcycle")
           .WithDisplayName("DeleteMotorcycle")
           .Produces(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status400BadRequest)
           .WithTags("Motorcycle")
           .RequireAuthorization("Admin");

    }
}
