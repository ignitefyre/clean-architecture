using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result>
{
    private readonly ICartRepository _repository;

    public UpdateItemCommandHandler(ICartRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var (productId, quantity, cartId) = request;
        
        var result = await _repository.GetById(cartId);
        
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
        
        return await _repository.Update(cart);
    }
}