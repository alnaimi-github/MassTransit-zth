using Orders.Application.Data.Entities;
using OrdersApi.Models;

namespace OrdersApi.Infrastructure;

public static class Extensions
{
    public static Order ToOrder(this OrderModel model)
    {
        return new Order
        {
            Comments = model.Comments,
            CreatedAt = DateTime.UtcNow,
            Total = model.Total
        };

    }
}
