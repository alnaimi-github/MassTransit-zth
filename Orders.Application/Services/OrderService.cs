using MassTransit;
using Orders.Application.Data;
using Orders.Application.Data.Entities;
using Orders.Application.Events;

namespace Orders.Application.Services
{
    public class OrderService(
        IPublishEndpoint publishEndpoint,
        OrdersDbContext dbContext)
        : IOrderService
    {
        public async Task<bool> CreateOrder(Order order)
        {
            //save the item in the database
            await dbContext.Orders.AddAsync(order);

            //publish an orderCreated event
            var orderCreatedEvent = new OrderCreated()
            {
                CreatedAt = order.CreatedAt,
                OrderId = order.Id
            };

            await publishEndpoint.Publish(orderCreatedEvent);

            var result = await dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
