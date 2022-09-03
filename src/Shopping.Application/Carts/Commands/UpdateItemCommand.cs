using FluentResults;
using MediatR;

namespace Shopping.Application.Carts.Commands;

public record UpdateItemCommand(string ProductId, int Quantity, string CartId) : IRequest<Result>;