namespace Shopping.Api.Models;

public record SuccessResponseData(string Id)
{
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
}