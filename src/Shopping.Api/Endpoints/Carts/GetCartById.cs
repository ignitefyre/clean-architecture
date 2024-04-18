using AutoMapper;
using MediatR;
using Shopping.Api.Models;
using Shopping.Application.Carts.Queries;
using Shopping.Domain.Errors;

namespace Shopping.Api.Endpoints.Carts;

public class GetCartById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/carts/{cartId}", async (string cartId, IMediator mediator, IMapper mapper, ILogger<GetCartById> logger, HttpContext context) =>
        {
            try
            {
                var result = await mediator.Send(new GetCartByIdQuery(cartId));

                if (result.IsFailed && result.HasError<CartNotFoundError>())
                    return Results.NotFound();
                
                var cart = result.Value;

                return result.IsFailed
                    ? Results.StatusCode(500)
                    : Results.Ok(mapper.Map<CartResponse>(cart));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error getting the cart");
                return Results.StatusCode(500);
            }
        });
    }
}