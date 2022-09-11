namespace Shopping.Api.Models;

public record CartCreatedResponse(string Id) : SuccessResponse(Id, "1.0");