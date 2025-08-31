using HelloApi.Models;
using HelloApi.Models.DTOs;

namespace MessageApi.Repositories
{
    public interface IItemRepository
    {
        Task<Item> AddItemAsync(ItemCreateDto dto);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task<Item?> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(int id);
    }
}
