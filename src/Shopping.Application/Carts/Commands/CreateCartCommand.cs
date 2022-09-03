using FluentResults;
using MediatR;

namespace Shopping.Application.Carts.Commands;

public record CreateCartCommand() : IRequest<Result<string>>;