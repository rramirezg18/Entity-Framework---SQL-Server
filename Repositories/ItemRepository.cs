using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Repositories
{
    public class ItemRepository(AppDbContext context) : IItemRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Item> AddItemAsync(ItemCreateDto dto)
        {
            var entity = new Item
            {
                Name = dto.Name,
                Price = dto.Price,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = 0,
                UpdatedAt = null
            };
            _context.Items.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items
                .OrderBy(i => i.Id)
                .ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<Item?> UpdateItemAsync(Item item)
        {
            var existing = await _context.Items.FindAsync(item.Id);
            if (existing == null) return null;

            existing.Name = item.Name;
            existing.Price = item.Price;
            existing.UpdatedBy = item.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var entity = await _context.Items.FindAsync(id);
            if (entity == null) return false;

            _context.Items.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
