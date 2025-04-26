using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Services;
using OrdersApi.Infrastructure;
using OrdersApi.Models;

namespace OrdersApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderModel model)
    {
        var createdOrder = await service.CreateOrder(model.ToOrder());
        return Accepted();
    }
}