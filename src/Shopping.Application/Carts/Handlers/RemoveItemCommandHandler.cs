using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;
using Shopping.Domain.Errors;

namespace Shopping.Application.Carts.Handlers;

public class RemoveItemCommandHandler(ICartRepository repository, IUserContext userContext) : IRequestHandler<RemoveItemCommand, Result>
{
    public async Task<Result> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
    {
        var (productId, cartId) = request;
        
        var result = await repository.GetById(cartId);
        
        if (result.IsFailed)
            return result.ToResult();

        var cart = result.Value;
        
        if (cart.OwnerId != userContext.UserId)
            return Result.Fail(new CartAccessDeniedError());
        
        cart.RemoveItem(productId);

        return await repository.Update(cart);
    }
}