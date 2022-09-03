using FluentResults;
using MediatR;

namespace Shopping.Application.Carts.Commands;

public record RemoveItemCommand(string ProductId, string CartId) : IRequest<Result>;