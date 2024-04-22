using MediatR;
using Microsoft.AspNetCore.Authorization;
using Shopping.Api.Models;
using Shopping.Application.Carts.Commands;
using Shopping.Domain.Errors;

namespace Shopping.Api.Endpoints.Carts;

public class DeleteCartItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/carts/{cartId}/items/{productId}", [Authorize] async (string cartId, string productId, IMediator mediator, ILogger<DeleteCartItem> logger, HttpContext context) =>
        {
            try
            {
                var result = await mediator.Send(new RemoveItemCommand(productId, cartId));
                
                if (result.IsFailed && result.HasError<CartNotFoundError>())
                    return Results.NotFound();
                
                return result.IsFailed ? Results.StatusCode(500) : Results.Ok(new CartUpdatedResponse(cartId));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error deleting item from the cart");
                return Results.StatusCode(500);
            }
        })
            .WithTags("Cart Items")
            .WithName("DeleteCartItem")
            .Produces<CartUpdatedResponse>(200)
            .Produces(404)
            .Produces(500)
            .RequireAuthorization("RequireWriteCarts");
    }
}