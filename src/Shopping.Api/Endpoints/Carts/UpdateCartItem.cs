using MediatR;
using Microsoft.AspNetCore.Authorization;
using Shopping.Api.Models;
using Shopping.Application.Carts.Commands;
using Shopping.Domain.Errors;

namespace Shopping.Api.Endpoints.Carts;

public class UpdateCartItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/carts/{cartId}/items/{productId}", [Authorize] async (string cartId, string productId, UpdateItemRequest request, IMediator mediator, ILogger<UpdateCartItem> logger, HttpContext context) =>
        {
            try
            {
                var result = await mediator.Send(new UpdateItemCommand(productId, request.Quantity, cartId));
                
                if (result.IsFailed && result.HasError<CartNotFoundError>())
                    return Results.NotFound();
                
                if (result.IsFailed && result.HasError<CartAccessDeniedError>())
                    return Results.Unauthorized();

                return result.IsFailed ? Results.StatusCode(500) : Results.Ok(new CartUpdatedResponse(cartId));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error updating item on the cart");
                return Results.StatusCode(500);
            }
        })
            .WithTags("Cart Items")
            .WithName("UpdateCartItem")
            .Produces<CartUpdatedResponse>(200)
            .Produces(404)
            .Produces(500)
            .RequireAuthorization("RequireWriteCarts");
    }
}