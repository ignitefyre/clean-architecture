using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, Result>
{
    private readonly ICartRepository _repository;

    public RemoveItemCommandHandler(ICartRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
    {
        var (productId, cartId) = request;
        
        var result = await _repository.GetById(cartId);
        
        if (result.IsFailed)
            return result.ToResult();

        var cart = result.Value;
        
        cart.RemoveItem(productId);

        return await _repository.Update(cart);
    }
}