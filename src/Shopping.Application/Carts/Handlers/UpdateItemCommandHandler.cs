using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result>
{
    public Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}