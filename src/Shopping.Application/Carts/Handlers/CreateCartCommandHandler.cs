using FluentResults;
using MediatR;
using Shopping.Application.Carts.Commands;

namespace Shopping.Application.Carts.Handlers;

public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Result<string>>
{
    private readonly ICartRepository _repository;

    public CreateCartCommandHandler(ICartRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<string>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var result = _repository.Create();

        return result.IsFailed ?
            result.ToResult<string>() :
            Result.Ok(result.Value.Id);
    }
}