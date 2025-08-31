using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Repositories;

namespace HelloApi.Services
{
    public class ItemService(IItemRepository repository) : IItemService
    {
        private readonly IItemRepository _repository = repository;

        public async Task<ItemReadDto> CreateItemAsync(ItemCreateDto dto)
        {
            var entity = await _repository.AddItemAsync(dto);
            return MapToReadDto(entity);
        }

        public async Task<IEnumerable<ItemReadDto>> GetAllItemsAsync()
        {
            var list = await _repository.GetAllItemsAsync();
            return list.Select(MapToReadDto);
        }

        public async Task<ItemReadDto?> GetItemByIdAsync(int id)
        {
            var entity = await _repository.GetItemByIdAsync(id);
            return entity == null ? null : MapToReadDto(entity);
        }

        public async Task<ItemReadDto?> UpdateItemAsync(int id, ItemUpdateDto dto)
        {
            var entity = new Item
            {
                Id = id,
                Name = dto.Name,
                Price = dto.Price,
                UpdatedBy = dto.UpdatedBy
            };

            var updated = await _repository.UpdateItemAsync(entity);
            return updated == null ? null : MapToReadDto(updated);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            return await _repository.DeleteItemAsync(id);
        }

        private static ItemReadDto MapToReadDto(Item i) => new()
        {
            Id = i.Id,
            Name = i.Name,
            Price = i.Price,
            CreatedBy = i.CreatedBy,
            CreatedAt = i.CreatedAt,
            UpdatedBy = i.UpdatedBy,
            UpdatedAt = i.UpdatedAt
        };
    }
}
