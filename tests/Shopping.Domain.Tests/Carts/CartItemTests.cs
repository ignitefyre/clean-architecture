using FluentAssertions;
using Shopping.Domain.Carts;

namespace Shopping.Domain.Tests.Carts;

public class CartItemTests : TestBase
{
    [Test]
    public void ShouldHaveExpectedQuantity()
    {
        //arrange
        const int quantity = 2;
        var result = new CartItem("abc", quantity);

        //assert
        result.Quantity.Should().Be(quantity);
    }

    [Test]
    public void ShouldUpdateExpectedQuantity()
    {
        //arrange
        const int quantityToUpdate = 5;
        var sut = new CartItem("abc", 1);

        //act
        sut.UpdateQuantity(quantityToUpdate);

        //assert
        sut.Quantity.Should().Be(quantityToUpdate);
    }
}