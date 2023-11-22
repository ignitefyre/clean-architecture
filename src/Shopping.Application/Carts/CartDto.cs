namespace Shopping.Application.Carts;

public record CartDto(string Id, ICollection<CartItemDto> Items, double Total, DateTime ModifiedOn);