namespace Shopping.Api.Models;

public record CartUpdatedResponse(string Id) : SuccessResponse(Id, "1.0");