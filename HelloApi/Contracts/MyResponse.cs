namespace HelloApi.Contracts;

public class MyResponse
{
    public string ResponseContent { get; set; } = string.Empty;
    public Guid InitialRequestId { get; set; }
}