namespace Shopping.Application.Carts;

public record CartDto(string Id, ICollection<CartItemDto> Items, DateTime ModifiedOn);