using AutoMapper;
using MediatR;
using Shopping.Api.Extensions;
using Shopping.Api.Models;
using Shopping.Application.Carts.Commands;
using Shopping.Application.Carts.Queries;
using Shopping.Domain.Errors;

namespace Shopping.Api.Endpoints.Carts;

public class AddCartItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts/{cartId}/items", async (string cartId, AddItemRequest request, IMediator mediator, IMapper mapper, ILogger<AddCartItem> logger, HttpContext context) =>
        {
            try
            {
                var (productId, quantity) = request;
                
                var result = await mediator.Send(new AddItemCommand(productId, quantity, cartId));

                if (result.IsFailed && result.HasError<CartNotFoundError>())
                    return Results.NotFound();
                
                return result.IsFailed ? Results.StatusCode(500) : Results.Created(
                    context.Request.AsCartItemResourceUri(cartId, productId),
                    new CartUpdatedResponse(cartId));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error adding item to the cart");
                return Results.StatusCode(500);
            }
        });
    }
}