using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Repositories;

namespace HelloApi.Services
{
    public class OrderDetailService(IOrderDetailRepository repository) : IOrderDetailService
    {
        private readonly IOrderDetailRepository _repository = repository;

        public async Task<OrderDetailReadDto> CreateOrderDetailAsync(OrderDetailCreateDto dto)
        {
            var entity = await _repository.AddOrderDetailAsync(dto);
            var withItem = await _repository.GetOrderDetailByIdAsync(entity.Id) ?? entity;
            return MapToReadDto(withItem);
        }

        public async Task<IEnumerable<OrderDetailReadDto>> GetAllOrderDetailsAsync()
        {
            var list = await _repository.GetAllOrderDetailsAsync();
            return list.Select(MapToReadDto);
        }

        public async Task<OrderDetailReadDto?> GetOrderDetailByIdAsync(int id)
        {
            var entity = await _repository.GetOrderDetailByIdAsync(id);
            return entity == null ? null : MapToReadDto(entity);
        }

        public async Task<OrderDetailReadDto?> UpdateOrderDetailAsync(int id, OrderDetailUpdateDto dto)
        {
            var existing = await _repository.GetOrderDetailByIdAsync(id);
            if (existing == null) return null;

            var entity = new OrderDetail
            {
                Id       = id,
                OrderId  = existing.OrderId, // <- requerido
                ItemId   = dto.ItemId,
                Quantity = dto.Quantity,
                Price    = dto.Price,
                Total    = decimal.Round(dto.Price * dto.Quantity, 2, MidpointRounding.AwayFromZero), // <- requerido
                UpdatedBy = dto.UpdatedBy
            };

            var updated = await _repository.UpdateOrderDetailAsync(entity);
            return updated == null ? null : MapToReadDto(updated);
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            return await _repository.DeleteOrderDetailAsync(id);
        }

        private static OrderDetailReadDto MapToReadDto(OrderDetail d) => new()
        {
            Id        = d.Id,
            OrderId   = d.OrderId,
            ItemId    = d.ItemId,
            ItemName  = d.Item?.Name,
            Quantity  = d.Quantity,
            Price     = d.Price,
            Total     = d.Total,
            CreatedBy = d.CreatedBy,
            CreatedAt = d.CreatedAt,
            UpdatedBy = d.UpdatedBy,
            UpdatedAt = d.UpdatedAt
        };
    }
}
