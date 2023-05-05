namespace Shopping.Api.Models;

public record AddItemRequest(string ProductId, int Quantity) : CartItem(ProductId, Quantity);