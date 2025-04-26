namespace Orders.Application.Events;

public class OrderCreated
{
    public DateTime CreatedAt { get; set; }
    public Guid OrderId { get; set; }
}