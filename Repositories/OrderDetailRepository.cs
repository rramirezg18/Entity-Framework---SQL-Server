using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Repositories
{
    public class OrderDetailRepository(AppDbContext context) : IOrderDetailRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<OrderDetail> AddOrderDetailAsync(OrderDetailCreateDto dto)
        {
            // Validaciones básicas
            var orderExists = await _context.Orders.AnyAsync(o => o.Id == dto.OrderId);
            if (!orderExists) throw new ArgumentException($"Order {dto.OrderId} not found.");

            var itemExists = await _context.Items.AnyAsync(i => i.Id == dto.ItemId);
            if (!itemExists) throw new ArgumentException($"Item {dto.ItemId} not found.");

            var entity = new OrderDetail
            {
                OrderId   = dto.OrderId,
                ItemId    = dto.ItemId,
                Quantity  = dto.Quantity,
                Price     = dto.Price,
                Total     = decimal.Round(dto.Price * dto.Quantity, 2, MidpointRounding.AwayFromZero),
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = 0,
                UpdatedAt = null
            };

            _context.OrderDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails
                .AsNoTracking()
                .Include(od => od.Item)
                .OrderBy(od => od.Id)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails
                .Include(od => od.Item)
                .FirstOrDefaultAsync(od => od.Id == id);
        }

        public async Task<OrderDetail?> UpdateOrderDetailAsync(OrderDetail data)
        {
            var existing = await _context.OrderDetails.FindAsync(data.Id);
            if (existing == null) return null;

            // (Opcional) si permites cambiar el Item:
            var itemExists = await _context.Items.AnyAsync(i => i.Id == data.ItemId);
            if (!itemExists) throw new ArgumentException($"Item {data.ItemId} not found.");

            existing.ItemId   = data.ItemId;
            existing.Quantity = data.Quantity;
            existing.Price    = data.Price;
            existing.Total    = decimal.Round(data.Price * data.Quantity, 2, MidpointRounding.AwayFromZero);
            existing.UpdatedBy = data.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            var entity = await _context.OrderDetails.FindAsync(id);
            if (entity == null) return false;

            _context.OrderDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
