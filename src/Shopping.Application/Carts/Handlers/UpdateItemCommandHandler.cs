using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class UpdateItemCommandHandler(ICartRepository repository) : IRequestHandler<UpdateItemCommand, Result>
{
    public async Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var (productId, quantity, cartId) = request;
        
        var result = await repository.GetById(cartId);
        
        if (result.IsFailed)
            return result.ToResult();

        var cart = result.Value;
        
        if (request.Quantity > 0)
        {
            cart.UpdateItemQuantity(productId, quantity);
        }
        else
        {
            cart.RemoveItem(request.ProductId);
        }
        
        return await repository.Update(cart);
    }
}