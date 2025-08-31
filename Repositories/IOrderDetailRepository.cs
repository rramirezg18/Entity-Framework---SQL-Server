using HelloApi.Models;
using HelloApi.Models.DTOs;

namespace MessageApi.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> AddOrderDetailAsync(OrderDetailCreateDto dto);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task<OrderDetail?> GetOrderDetailByIdAsync(int id);
        Task<OrderDetail?> UpdateOrderDetailAsync(OrderDetail detail);
        Task<bool> DeleteOrderDetailAsync(int id);
    }
}
