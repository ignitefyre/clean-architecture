using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, Result>
{
    public Task<Result> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}