using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Result<string>>
{
    public Task<Result<string>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}