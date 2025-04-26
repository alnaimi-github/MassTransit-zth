using HelloApi.Contracts;
using MassTransit;

namespace HelloApi.Consumers;

public class MyRequestConsumer : IConsumer<MyRequest>
{
    public async Task Consume(ConsumeContext<MyRequest> context)
    {
        await context.RespondAsync<MyResponse>(new MyResponse
        {
            InitialRequestId = context.Message.Id,
            ResponseContent = $"I got: {context.Message.RequestBody} and I send it back"

        }); ;

    }
}