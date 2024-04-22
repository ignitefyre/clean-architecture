using AutoMapper;
using FluentResults;
using Shopping.Application.Products;
using Shopping.Domain.Errors;
using Shopping.Domain.Products;

namespace Shopping.Infrastructure.Products;

public class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private static readonly List<ProductData> ProductsInMemory = new List<ProductData>();

    public ProductRepository(IMapper mapper)
    {
        _mapper = mapper;
        
        ProductsInMemory.Add(new ProductData("123", 5.99));
        ProductsInMemory.Add(new ProductData("345", 10.99));
    }
    public Task<Result<Product>> Create(string ownerName)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Product>> GetById(string id)
    {
        var product = ProductsInMemory.FirstOrDefault(x => x.Id == id);
        
        return product == null ? Result.Fail(new ProductNotFoundError()) : Result.Ok(_mapper.Map<Product>(product));
    }

    public Task<Result> Update(Product entity)
    {
        throw new NotImplementedException();
    }
}