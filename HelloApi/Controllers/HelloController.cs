using HelloApi.Contracts;
using HelloApi.Filters;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly Tenant _tenant;
    private readonly IRequestClient<MyRequest> _requestClient;

    public HelloController(IPublishEndpoint publishEndpoint,
        ISendEndpointProvider sendEndpointProvider,
        Tenant tenant,
        IRequestClient<MyRequest> requestClient
    )
    {
        this._publishEndpoint = publishEndpoint;
        this._sendEndpointProvider = sendEndpointProvider;
        this._tenant = tenant;
        this._requestClient = requestClient;
        this._tenant.MyValue = "MyCoolTenant";
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var messageToSend = new Message() { Text = "Hello from api" };
        await _publishEndpoint.Publish(messageToSend);

        //await publishEndpoint.Publish(new
        //Email
        //{
        //    Destination = "",
        //    Subject = "Hello from an email!"
        //});

        //await publishEndpoint.Publish<Message>(messageToSend, publishContext =>
        //{
        //    publishContext.Headers.Set("Publish-Context", "en-us");
        //    publishContext.SetRoutingKey("my-direct-routing-key");
        //});

        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:my-queue"));
        //await sendEndpoint.Send(messageToSend);
        //await sendEndpoint.Send<Message>(new
        //{
        //    messageToSend.Text
        //},
        //sendContext =>
        //{
        //    sendContext.Headers.Set("Culture", "en-us");

        //});

        return Ok();
    }

    [HttpGet("/request")]
    public async Task<IActionResult> RequestAction()
    {
        var myResponse = await _requestClient.GetResponse<MyResponse>(
            new MyRequest()
            {
                Id= Guid.NewGuid(),
                RequestBody = "My request body"
            });

        return Ok(myResponse.Message);
    }
}