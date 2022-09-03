using FluentResults;
using MediatR;
using Shopping.Application.Carts.Queries;

namespace Shopping.Application.Carts.Handlers;

public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, Result<CartDto>>
{
    public Task<Result<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}