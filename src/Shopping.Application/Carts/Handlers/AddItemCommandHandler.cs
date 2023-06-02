using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, Result>
{
    private readonly ICartRepository _repository;
    private readonly IEventPublisher _eventPublisher;

    public AddItemCommandHandler(ICartRepository repository, IEventPublisher eventPublisher)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
    }
    
    public async Task<Result> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var (productId, quantity, cartId) = request;

        var result = await _repository.GetById(cartId);
        
        if (result.IsFailed)
            return result.ToResult();
        
        var cart = result.Value;
        
        cart.AddItem(productId, quantity);
        
        var updatedResult = await _repository.Update(cart);
        
        if (updatedResult.IsFailed)
            return updatedResult;
        
        foreach (var @event in cart.Events)
        {
            await _eventPublisher.Publish(@event);
        }

        return Result.Ok();
    }
}