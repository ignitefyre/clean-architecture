namespace Shopping.Api.Models;

public record CartResponseData(string Id, List<CartItem> Items) : SuccessResponseData(Id);