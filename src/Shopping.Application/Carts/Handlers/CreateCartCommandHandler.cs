using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class CreateCartCommandHandler(ICartRepository repository, IUserContext userContext) : IRequestHandler<CreateCartCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.Create(userContext.UserId);

        return result.IsFailed ?
            result.ToResult<string>() :
            Result.Ok(result.Value.Id);
    }
}