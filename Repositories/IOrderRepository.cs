using HelloApi.Models;
using HelloApi.Models.DTOs;

namespace MessageApi.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(OrderCreateDto dto);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order?> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
