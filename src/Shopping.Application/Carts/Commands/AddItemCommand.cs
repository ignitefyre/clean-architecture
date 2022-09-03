using FluentResults;
using MediatR;

namespace Shopping.Application.Carts.Commands;

public record AddItemCommand(string ProductId, int Quantity, string CartId) : IRequest<Result>;