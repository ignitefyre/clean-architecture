using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;
using Shopping.Domain.Errors;

namespace Shopping.Application.Carts.Handlers;

public class UpdateItemCommandHandler(ICartRepository repository, IUserContext userContext) : IRequestHandler<UpdateItemCommand, Result>
{
    public async Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var (productId, quantity, cartId) = request;
        
        var result = await repository.GetById(cartId);
        
        if (result.IsFailed)
            return result.ToResult();

        var cart = result.Value;
        
        if (cart.OwnerId != userContext.UserId)
            return Result.Fail(new CartAccessDeniedError());
        
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