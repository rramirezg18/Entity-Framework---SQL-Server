using HelloApi.Models.DTOs;

namespace HelloApi.Services
{
    public interface IItemService
    {
        Task<ItemReadDto> CreateItemAsync(ItemCreateDto dto);
        Task<IEnumerable<ItemReadDto>> GetAllItemsAsync();
        Task<ItemReadDto?> GetItemByIdAsync(int id);
        Task<ItemReadDto?> UpdateItemAsync(int id, ItemUpdateDto dto);
        Task<bool> DeleteItemAsync(int id);
    }
}
