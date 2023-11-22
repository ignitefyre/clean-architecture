namespace Shopping.Api.Models;

public record CartResponseData(string Id, double Total, List<CartItem> Items) : SuccessResponseData(Id);