namespace HelloApi.Contracts;

public class MyRequest
{
    public Guid Id { get; set; }
    public string RequestBody { get; set; } = string.Empty;
}