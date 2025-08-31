using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Repositories;

namespace HelloApi.Services
{
    public class OrderService(IOrderRepository repository) : IOrderService
    {
        private readonly IOrderRepository _repository = repository;

        public async Task<OrderReadDto> CreateOrderAsync(OrderCreateDto dto)
        {
            var entity = await _repository.AddOrderAsync(dto);
            // Trae con include para mapear nombre (opcional)
            var withPerson = await _repository.GetOrderByIdAsync(entity.Id) ?? entity;
            return MapToReadDto(withPerson);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var list = await _repository.GetAllOrdersAsync();
            return list.Select(MapToReadDto);
        }

        public async Task<OrderReadDto?> GetOrderByIdAsync(int id)
        {
            var entity = await _repository.GetOrderByIdAsync(id);
            return entity == null ? null : MapToReadDto(entity);
        }

        public async Task<OrderReadDto?> UpdateOrderAsync(int id, OrderUpdateDto dto)
        {
            var entity = new Order
            {
                Id = id,
                PersonId = dto.PersonId,
                Number = dto.Number,
                UpdatedBy = dto.UpdatedBy
            };

            var updated = await _repository.UpdateOrderAsync(entity);
            return updated == null ? null : MapToReadDto(updated);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _repository.DeleteOrderAsync(id);
        }

        private static OrderReadDto MapToReadDto(Order o) => new()
        {
            Id = o.Id,
            PersonId = o.PersonId,
            PersonFullName = o.Person != null ? $"{o.Person.FirstName} {o.Person.LastName}" : null,
            Number = o.Number,
            CreatedBy = o.CreatedBy,
            CreatedAt = o.CreatedAt,
            UpdatedBy = o.UpdatedBy,
            UpdatedAt = o.UpdatedAt
        };
    }
}
