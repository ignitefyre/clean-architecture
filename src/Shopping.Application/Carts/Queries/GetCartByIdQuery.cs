using FluentResults;
using MediatR;

namespace Shopping.Application.Carts.Queries;

public record GetCartByIdQuery(string Id) : IRequest<Result<CartDto>>;