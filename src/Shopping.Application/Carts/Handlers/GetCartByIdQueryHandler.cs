using AutoMapper;
using FluentResults;
using MediatR;
using Shopping.Application.Carts.Queries;
using Shopping.Domain.Errors;

namespace Shopping.Application.Carts.Handlers;

public class GetCartByIdQueryHandler(ICartRepository repository, IMapper mapper, IUserContext userContext)
    : IRequestHandler<GetCartByIdQuery, Result<CartDto>>
{
    public async Task<Result<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetById(request.Id);

        if (result.IsFailed)
            return result.ToResult<CartDto>();
        
        var cart = result.Value;
        
        if (cart.OwnerId != userContext.UserId)
            return Result.Fail(new CartAccessDeniedError());

        var response = mapper.Map<CartDto>(cart);

        return Result.Ok(response);
    }
}