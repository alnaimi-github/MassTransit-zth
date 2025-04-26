namespace HelloApi.Orders.Contracts;

public class OrderResult
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string CustomerName { get; set; } = string.Empty;
}