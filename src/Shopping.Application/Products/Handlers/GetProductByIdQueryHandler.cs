using FluentResults;
using MediatR;
using Shopping.Application.Products.Queries;

namespace Shopping.Application.Products.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}