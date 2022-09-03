using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, Result>
{
    public Task<Result> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}