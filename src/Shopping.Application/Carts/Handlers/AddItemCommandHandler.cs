using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;
using Shopping.Domain.Errors;

namespace Shopping.Application.Carts.Handlers;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, Result>
{
    private readonly ICartRepository _repository;

    public AddItemCommandHandler(ICartRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var (productId, quantity, cartId) = request;

        var result = await _repository.GetById(cartId);

        if (result.IsFailed)
            return result.ToResult();

        var cart = result.Value;
        
        cart.AddItem(productId, quantity);

        return await _repository.Update(cart);
    }
}