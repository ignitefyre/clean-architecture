using AutoMapper;
using FluentAssertions;
using FluentResults;
using Moq;
using RandomTestValues;
using Shopping.Application.Carts;
using Shopping.Application.Carts.Handlers;
using Shopping.Application.Carts.Queries;
using Shopping.Domain.Carts;

namespace Shopping.Application.Tests.Carts;

public class GetCartByIdQueryHandlerTests : TestBase
{
    [Test]
    public async Task ShouldReturnACartDtoResult()
    {
        //arrange
        var cartId = RandomValue.String();
        var cartItems = new List<CartItem>
        {
            new CartItem(RandomValue.String(), RandomValue.Int())
        };
        var cart = new Cart(cartId, cartItems, DateTime.UtcNow);
        
        var cartRepositoryMock = new Mock<ICartRepository>();
        cartRepositoryMock
            .Setup(x => x.GetById(cartId))
            .ReturnsAsync(Result.Ok(cart));

        var mapperResult = new CartDto(
            cartId,
            new List<CartItemDto>
            {
                new CartItemDto(RandomValue.String(), RandomValue.Int())
            },
            cart.ModifiedOn);

        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(x => x.Map<CartDto>(cart))
            .Returns(mapperResult);
        
        var sut = new GetCartByIdQueryHandler(cartRepositoryMock.Object, mapperMock.Object);

        //act
        var request = new GetCartByIdQuery(cartId);
        var result = await sut.Handle(request, CancellationToken.None);
        
        //assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(mapperResult.Id);
        result.Value.Items.Should().BeSameAs(mapperResult.Items);
        result.Value.ModifiedOn.Should().Be(mapperResult.ModifiedOn);
    }
}