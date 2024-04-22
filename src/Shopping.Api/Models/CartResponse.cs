namespace Shopping.Api.Models;

public record CartResponse(string Id, CartResponseData Data, string OwnerId) : SuccessResponse(Id, "1.0");