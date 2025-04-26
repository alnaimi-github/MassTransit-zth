using Orders.Application.Data.Entities;

namespace Orders.Application.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(Order order);
    }
}