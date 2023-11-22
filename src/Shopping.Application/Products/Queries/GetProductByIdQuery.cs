using FluentResults;
using MediatR;
using Shopping.Application.Carts;

namespace Shopping.Application.Products.Queries;

public record GetProductByIdQuery(string Id) : IRequest<Result<ProductDto>>;