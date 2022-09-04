using FluentResults;
using MediatR;
using Shopping.Application.Carts.Queries;

namespace Shopping.Application.Carts.Handlers;

public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, Result<CartDto>>
{
    private readonly ICartRepository _repository;

    public GetCartByIdQueryHandler(ICartRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);

        if (result.IsFailed)
            return result.ToResult<CartDto>();

        var response = new CartDto(
            result.Value.Id,
            result.Value.GetItems().Select(x => new CartItemDto(x.Id, x.Quantity)).ToList(),
            result.Value.ModifiedOn);

        return Result.Ok(response);
    }
}