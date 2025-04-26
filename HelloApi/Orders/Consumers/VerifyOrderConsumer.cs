using HelloApi.Contracts;
using HelloApi.Orders.Contracts;
using MassTransit;

namespace HelloApi.Orders.Consumers;

public class VerifyOrderConsumer : IConsumer<VerifyOrder>
{
    public async Task Consume(ConsumeContext<VerifyOrder> context)
    {
        if (!context.IsResponseAccepted<Email>())
        {
            throw new ArgumentOutOfRangeException(nameof(context));
        }

        if (context.Message.Id == 1)
        {
            await context.RespondAsync<OrderResult>(new OrderResult
            {
                Id = context.Message.Id,
                Amount = 30,
                CustomerName = "waisa"

            });
        }
        else
        {
            await context.RespondAsync<OrderNotFoundResult>(
                new OrderNotFoundResult()
                {
                    Message = $"Order with Id :{context.Message.Id} not found, Sorrry!"
                });
        }
    }
}