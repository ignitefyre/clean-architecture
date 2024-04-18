using AutoMapper;
using MediatR;
using Shopping.Api.Extensions;
using Shopping.Api.Models;
using Shopping.Application.Carts.Commands;

namespace Shopping.Api.Endpoints.Carts;

public class CreateCart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts", async (IMediator mediator, ILogger<CreateCart> logger, HttpContext context) =>
        {
            try
            {
                var result = await mediator.Send(new CreateCartCommand());

                return result.IsFailed
                    ? Results.StatusCode(500)
                    : Results.Created(
                        context.Request.AsCartResourceUri(result.Value),
                        new CartCreatedResponse(result.Value));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error creating a cart");
                return Results.StatusCode(500);
            }
        });
    }
}