using MassTransit;
using Orders.Application.Events;
using System.Threading.Tasks;
using System;

namespace Orders.Worker.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            Console.WriteLine($"Order with id{context.Message.OrderId} consumed, I got it");
            return Task.CompletedTask;
        }
    }
}
