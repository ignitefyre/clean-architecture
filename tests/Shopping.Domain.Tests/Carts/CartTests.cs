using FluentAssertions;
using Shopping.Domain.Carts;

namespace Shopping.Domain.Tests.Carts;

public class CartTests : TestBase
{
    [Test]
    public void ANewCartShouldNotContainItems()
    {
        //arrange
        var sut = new Cart("abc");

        //act
        var results = sut.GetItems();
        
        //assert
        results.Should().BeEmpty();
    }
    
    [Test]
    public void ShouldAddAnItem()
    {
        //arrange
        var sut = new Cart("abc");
        
        //act
        sut.AddItem("123", 1);
        
        //assert
        sut.GetItems().Should().Contain(x => x.Id == "123");
        sut.GetItems().First(x => x.Id == "123").Quantity.Should().Be(1);
    }

    [Test]
    public void ShouldUpdateItemQuantity()
    {
        //arrange
        var sut = new Cart("abc", new List<CartItem>
        {
            new CartItem("123", 1)
        }, DateTime.UtcNow);
        
        //act
        sut.UpdateItemQuantity("123", 2);
        
        //assert
        sut.GetItems().Should().Contain(x => x.Id == "123");
        sut.GetItems().First(x => x.Id == "123").Quantity.Should().Be(2);
    }

    [Test]
    public void ShouldRemoveAnItem()
    {
        //arrange
        var sut = new Cart("abc", new List<CartItem>
        {
            new CartItem("123", 1)
        }, DateTime.UtcNow);
        
        //act
        sut.RemoveItem("123");
        
        //assert
        sut.GetItems().Should().BeEmpty();
        sut.GetItems().Should().NotContain(x => x.Id == "123");
    }
}