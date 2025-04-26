using HelloApi.Contracts;
using HelloApi.Orders.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IRequestClient<VerifyOrder> requestClient) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var response = await requestClient.GetResponse<OrderResult, OrderNotFoundResult, Email>(
            new
            {
                Id = id,
                __Header_Promotion = "some-promotional-discount-percent"

            }); ;

        if (response.Is(out Response<OrderResult>? order))
        {
            return Ok(order.Message);
        }

        if (response.Is(out Response<OrderNotFoundResult>? notFound))
        {
            return NotFound();
        }
        return BadRequest();
    }
}