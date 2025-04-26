namespace OrdersApi.Models
{
    public class OrderModel
    {
        public decimal Total { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
