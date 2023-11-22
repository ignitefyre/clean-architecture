namespace Shopping.Api.Models;

public record CartItem(string ProductId, int Quantity, double Price = 0);