using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;
using Shopping.Domain.Errors;

namespace Shopping.Application.Carts.Handlers;

public class AddItemCommandHandler(ICartRepository repository, IEventPublisher eventPublisher, IUserContext userContext)
    : IRequestHandler<AddItemCommand, Result>
{
    public async Task<Result> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var (productId, quantity, cartId) = request;

        var result = await repository.GetById(cartId);
        
        if (result.IsFailed)
            return result.ToResult();
        
        var cart = result.Value;

        if (cart.OwnerId != userContext.UserId)
            return Result.Fail(new CartAccessDeniedError());
        
        cart.AddItem(productId, quantity);
        
        var updatedResult = await repository.Update(cart);
        
        if (updatedResult.IsFailed)
            return updatedResult;
        
        foreach (var @event in cart.Events)
        {
            await eventPublisher.Publish(@event);
        }

        return Result.Ok();
    }
}