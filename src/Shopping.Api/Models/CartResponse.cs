namespace Shopping.Api.Models;

public record CartResponse(string Id, CartResponseData Data, string OwnerName) : SuccessResponse(Id, "1.0");