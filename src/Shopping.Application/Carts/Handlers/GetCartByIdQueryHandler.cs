using AutoMapper;
using FluentResults;
using MediatR;
using Shopping.Application.Carts.Queries;

namespace Shopping.Application.Carts.Handlers;

public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, Result<CartDto>>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public GetCartByIdQueryHandler(ICartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Result<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);

        if (result.IsFailed)
            return result.ToResult<CartDto>();

        var response = _mapper.Map<CartDto>(result.Value);

        return Result.Ok(response);
    }
}