using FluentAssertions;
using Shopping.Domain.Carts;
using Shopping.Domain.Events;

namespace Shopping.Domain.Tests.Carts;

public class CartTests : TestBase
{
    [Test]
    public void ANewCartShouldNotContainItems()
    {
        //arrange
        var sut = new Cart();

        //act
        var results = sut.GetItems();
        
        //assert
        results.Should().BeEmpty();
        
        sut.Events.Count.Should().Be(1);
        sut.Events.First().Id.Should().NotBeEmpty();
        sut.Events.First().Type.Should().Be("Shopping.Cart.Created.v1");
        sut.Events.First().Source.Should().Be($"urn:cart:{sut.Id}");
        sut.Events.First().GetData().Should().BeNull();
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

        sut.Events.Count.Should().Be(1);
        sut.Events.First().Id.Should().NotBeEmpty();
        sut.Events.First().Type.Should().Be("Shopping.Cart.ItemAdded.v1");
        sut.Events.First().Source.Should().Be($"urn:cart:abc");
        sut.Events.First().GetData().Should().BeEquivalentTo(new { CartId = "abc", Quantity = 1, ProductId = "123" });
    }

    [Test]
    public void ShouldUpdateItemQuantity()
    {
        //arrange
        var sut = new Cart("abc", new List<CartItem>
        {
            new CartItem("123", 1)
        }, DateTime.UtcNow, "owner name");
        
        //act
        sut.UpdateItemQuantity("123", 2);
        
        //assert
        sut.GetItems().Should().Contain(x => x.Id == "123");
        sut.GetItems().First(x => x.Id == "123").Quantity.Should().Be(2);
        
        sut.Events.Count.Should().Be(1);
        sut.Events.First().Id.Should().NotBeEmpty();
        sut.Events.First().Type.Should().Be("Shopping.Cart.ItemQuantityUpdated.v1");
        sut.Events.First().Source.Should().Be($"urn:cart:abc");
        sut.Events.First().GetData().Should().BeEquivalentTo(new { CartId = "abc", Quantity = 2, ProductId = "123" });
    }

    [Test]
    public void ShouldRemoveAnItem()
    {
        //arrange
        var sut = new Cart("abc", new List<CartItem>
        {
            new CartItem("123", 1)
        }, DateTime.UtcNow, "owner name");
        
        //act
        sut.RemoveItem("123");
        
        //assert
        sut.GetItems().Should().BeEmpty();
        sut.GetItems().Should().NotContain(x => x.Id == "123");
        
        sut.Events.Count.Should().Be(1);
        sut.Events.First().Id.Should().NotBeEmpty();
        sut.Events.First().Type.Should().Be("Shopping.Cart.ItemRemoved.v1");
        sut.Events.First().Source.Should().Be($"urn:cart:abc");
        sut.Events.First().GetData().Should().BeEquivalentTo(new { CartId = "abc", ProductId = "123" });
    }
}