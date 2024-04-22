using FluentAssertions;
using FluentResults;
using Moq;
using RandomTestValues;
using Shopping.Application.Carts;
using Shopping.Application.Carts.Commands;
using Shopping.Application.Carts.Handlers;
using Shopping.Domain;
using Shopping.Domain.Carts;
using Shopping.Domain.Events;

namespace Shopping.Application.Tests.Carts;

public class AddItemCommandHandlerTests : TestBase
{
    [Test]
    public async Task ShouldAddItemToExistingCartAndPublishEvents()
    {
        //arrange
        var cartId = RandomValue.String();
        var productId = RandomValue.String();
        var productQuantity = RandomValue.Int();
        var ownerName = RandomValue.String();
        
        var cartItems = new List<CartItem>();
        var cart = new Cart(cartId, cartItems, DateTime.UtcNow, ownerName);
        
        var cartRepositoryMock = new Mock<ICartRepository>();
        cartRepositoryMock
            .Setup(x => x.GetById(cartId))
            .ReturnsAsync(Result.Ok(cart));

        cartRepositoryMock
            .Setup(x => x.Update(cart))
            .ReturnsAsync(Result.Ok);

        var eventRepositoryMock = new Mock<IEventPublisher>();
        
        var sut = new AddItemCommandHandler(cartRepositoryMock.Object, eventRepositoryMock.Object);
        
        //act
        var request = new AddItemCommand(productId, productQuantity, cartId);
        var result = await sut.Handle(request, CancellationToken.None);
        
        //assert
        cartRepositoryMock.Verify(x => x.Update(cart), Times.Once);
        
        eventRepositoryMock.Verify(x => x.Publish(It.IsAny<IEvent>()), Times.Once);
    }
    
    [Test]
    public async Task ShouldIndicateFailureWhenPersistenceFails()
    {
        //arrange
        var cartId = RandomValue.String();
        var productId = RandomValue.String();
        var productQuantity = RandomValue.Int();
        var ownerName = RandomValue.String();
        
        var cartItems = new List<CartItem>();
        var cart = new Cart(cartId, cartItems, DateTime.UtcNow, ownerName);
        
        var cartRepositoryMock = new Mock<ICartRepository>();
        cartRepositoryMock
            .Setup(x => x.GetById(cartId))
            .ReturnsAsync(Result.Ok(cart));

        cartRepositoryMock
            .Setup(x => x.Update(cart))
            .ReturnsAsync(Result.Fail("Mock Failure"));

        var eventRepositoryMock = new Mock<IEventPublisher>();
        
        var sut = new AddItemCommandHandler(cartRepositoryMock.Object, eventRepositoryMock.Object);
        
        //act
        var request = new AddItemCommand(productId, productQuantity, cartId);
        var result = await sut.Handle(request, CancellationToken.None);
        
        //assert
        cartRepositoryMock.Verify(x => x.Update(cart), Times.Once);
        cartRepositoryMock.Verify(x => x.Update(It.Is<Cart>(i => i.GetItems().First().Quantity == productQuantity)));
        
        eventRepositoryMock.Verify(x => x.Publish(It.IsAny<IEvent>()), Times.Never);

        result.IsFailed.Should().BeTrue();
    }
}