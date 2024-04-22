using AutoMapper;
using FluentResults;
using MediatR;
using Shopping.Application.Carts.Queries;

namespace Shopping.Application.Carts.Handlers;

public class GetCartByIdQueryHandler(ICartRepository repository, IMapper mapper)
    : IRequestHandler<GetCartByIdQuery, Result<CartDto>>
{
    public async Task<Result<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetById(request.Id);

        if (result.IsFailed)
            return result.ToResult<CartDto>();

        var response = mapper.Map<CartDto>(result.Value);

        return Result.Ok(response);
    }
}