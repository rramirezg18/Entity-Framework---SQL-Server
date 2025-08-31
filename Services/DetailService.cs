using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Repositories;

namespace HelloApi.Services
{
    public class DetailService(IDetailRepository repository) : IDetailService
    {
        private readonly IDetailRepository _repository = repository;

        public async Task<DetailReadDto> CreateDetailAsync(DetailCreateDto dto)
        {
            var entity = await _repository.AddDetailAsync(dto);
            var withItem = await _repository.GetDetailByIdAsync(entity.Id) ?? entity;
            return MapToReadDto(withItem);
        }

        public async Task<IEnumerable<DetailReadDto>> GetAllDetailsAsync()
        {
            var list = await _repository.GetAllDetailsAsync();
            return list.Select(MapToReadDto);
        }

        public async Task<DetailReadDto?> GetDetailByIdAsync(int id)
        {
            var entity = await _repository.GetDetailByIdAsync(id);
            return entity == null ? null : MapToReadDto(entity);
        }

        public async Task<DetailReadDto?> UpdateDetailAsync(int id, DetailUpdateDto dto)
        {
            var entity = new Detail
            {
                Id = id,
                StatusId = dto.StatusId,
                Price = dto.Price,
                Quantity = dto.Quantity,
                ItemId = dto.ItemId
            };

            var updated = await _repository.UpdateDetailAsync(entity);
            return updated == null ? null : MapToReadDto(updated);
        }

        public async Task<bool> DeleteDetailAsync(int id)
        {
            return await _repository.DeleteDetailAsync(id);
        }

        private static DetailReadDto MapToReadDto(Detail d) => new()
        {
            Id = d.Id,
            StatusId = d.StatusId,
            Price = d.Price,
            Quantity = d.Quantity,
            Total = d.Total,
            CreatedAt = d.CreatedAt,
            UpdatedAt = d.UpdatedAt,
            ItemId = d.ItemId,
            ItemName = d.Item?.Name
        };
    }
}
