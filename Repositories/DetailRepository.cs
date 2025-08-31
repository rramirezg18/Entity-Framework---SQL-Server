using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Repositories
{
    public class DetailRepository(AppDbContext context) : IDetailRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Detail> AddDetailAsync(DetailCreateDto dto)
        {
            // Valida que el Item exista
            var itemExists = await _context.Items.AnyAsync(i => i.Id == dto.ItemId);
            if (!itemExists) throw new ArgumentException($"Item {dto.ItemId} not found.");

            var entity = new Detail
            {
                StatusId = dto.StatusId,
                Price = dto.Price,
                Quantity = dto.Quantity,
                Total = decimal.Round(dto.Price * dto.Quantity, 2, MidpointRounding.AwayFromZero),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                ItemId = dto.ItemId
            };

            _context.Details.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Detail>> GetAllDetailsAsync()
        {
            return await _context.Details
                .AsNoTracking()
                .Include(d => d.Item)
                .OrderBy(d => d.Id)
                .ToListAsync();
        }

        public async Task<Detail?> GetDetailByIdAsync(int id)
        {
            return await _context.Details
                .Include(d => d.Item)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Detail?> UpdateDetailAsync(Detail data)
        {
            var existing = await _context.Details.FindAsync(data.Id);
            if (existing == null) return null;

            // Valida que el Item exista (si permite cambiar ItemId)
            var itemExists = await _context.Items.AnyAsync(i => i.Id == data.ItemId);
            if (!itemExists) throw new ArgumentException($"Item {data.ItemId} not found.");

            existing.StatusId = data.StatusId;
            existing.Price = data.Price;
            existing.Quantity = data.Quantity;
            existing.Total = decimal.Round(data.Price * data.Quantity, 2, MidpointRounding.AwayFromZero);
            existing.ItemId = data.ItemId;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteDetailAsync(int id)
        {
            var entity = await _context.Details.FindAsync(id);
            if (entity == null) return false;

            _context.Details.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
