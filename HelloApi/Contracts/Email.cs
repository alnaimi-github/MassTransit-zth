namespace HelloApi.Contracts;

public class Email
{
    public string Destination { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
}