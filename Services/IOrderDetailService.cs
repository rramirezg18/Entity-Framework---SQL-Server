using HelloApi.Models.DTOs;

namespace HelloApi.Services
{
    public interface IOrderDetailService
    {
        Task<OrderDetailReadDto> CreateOrderDetailAsync(OrderDetailCreateDto dto);
        Task<IEnumerable<OrderDetailReadDto>> GetAllOrderDetailsAsync();
        Task<OrderDetailReadDto?> GetOrderDetailByIdAsync(int id);
        Task<OrderDetailReadDto?> UpdateOrderDetailAsync(int id, OrderDetailUpdateDto dto);
        Task<bool> DeleteOrderDetailAsync(int id);
    }
}
