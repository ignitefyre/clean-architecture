namespace Shopping.Api.Models;

public record CartResponse(string Id, string Version, CartResponseData Data) : SuccessResponse(Id, Version);