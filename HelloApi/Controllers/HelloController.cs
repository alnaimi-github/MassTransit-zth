using HelloApi.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HelloController(
    IPublishEndpoint publishEndpoint,
    ISendEndpointProvider senderEndpoint) 
    : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var message = new Message { Text = "Hello, World!" };
        await publishEndpoint.Publish(message);

        //await publishEndpoint.Publish<Message>(message, publishContext =>
        //{
        //    publishContext.Headers.Set("Publish-Context", "en-us");
        //    publishContext.SetRoutingKey("my-direct-routing-key");
        //});

        var sender = await senderEndpoint.GetSendEndpoint(new Uri("queue:my-queue"));
                     await sender.Send(message);

                     //await sendEndpoint.Send<Message>(new
                     //{
                     //    messageToSend.Text
                     //},
                     //sendContext =>
                     //{
                     //    sendContext.Headers.Set("Culture", "en-us");

                     //});

        return Ok("Message published");
    }
}