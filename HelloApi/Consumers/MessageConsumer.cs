using HelloApi.Contracts;
using MassTransit;

namespace HelloApi.Consumers;

public class MessageConsumer(ILogger<MessageConsumer> logger) : IConsumer<Message>
{
    public Task Consume(ConsumeContext<Message> context)
    {
       // throw new InvalidOperationException(nameof(context));
        logger.LogInformation($"Message consumed {context.Message.Text}");
       Task.Delay(10000);

        return Task.CompletedTask;
    }
}