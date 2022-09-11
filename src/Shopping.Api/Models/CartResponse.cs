namespace Shopping.Api.Models;

public record CartResponse(string Id, CartResponseData Data) : SuccessResponse(Id, "1.0");