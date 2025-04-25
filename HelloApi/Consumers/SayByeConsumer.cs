using HelloApi.Contracts;
using MassTransit;

namespace HelloApi.Consumers
{
    public class SayByeConsumer : IConsumer<Message>
    {
        public Task Consume(ConsumeContext<Message> context)
        {
            Console.WriteLine($"SayByeConsumers{context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
