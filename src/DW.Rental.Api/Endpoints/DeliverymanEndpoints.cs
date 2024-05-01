using DW.Rental.Shareable.Requests.Deliveryman;
using DW.Rental.Shareable.Requests.Deliveryman.Input;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DW.Rental.Api.Endpoints;

public static class DeliverymanEndpoints
{
    public static void MapDeliveryAppEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/deliveryman", async (IMediator mediator, [FromBody] CreateDeliverymanInput createDeliverymanInput) =>
            {
                var result = await mediator.Send(
                    new CreateDeliverymanRequest(
                        createDeliverymanInput.Nome,
                        createDeliverymanInput.Senha,
                        createDeliverymanInput.Cnpj,
                        DateOnly.FromDateTime(createDeliverymanInput.DataNascimento),
                        createDeliverymanInput.NumeroCnh,
                        createDeliverymanInput.TipoCnh));

                if (result.IsSuccess)
                {
                    return Results.Created($"api/v1/moto{result.Value.Id}", result.Value);
                }

                return Results.BadRequest("Erro");
            })
            .WithName("CreateDeliveryman")
            .WithDisplayName("CreateDeliveryman")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Deliveryman");


        app.MapPut("api/v1/deliveryman/{id}/photo", async (IMediator mediator, Guid id, [FromForm] IFormFile arquivo) =>
        {
            var request = new PutDeliverymanPhotoRequest(arquivo.OpenReadStream(), arquivo.FileName, id);

            var result = await mediator.Send(request);

            return Results.Ok("Imagem recebida e salva com sucesso!");
        })
            .WithName("PhotoDeliveryman")
            .WithDisplayName("PhotoDeliveryman")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithTags("Deliveryman")
            .RequireAuthorization("Deliveryman")
            .DisableAntiforgery();
    }
}
