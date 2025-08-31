using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Repositories
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Order> AddOrderAsync(OrderCreateDto dto)
        {
            // Valida existencia de la persona (opcional pero recomendado)
            var personExists = await _context.Persons.AnyAsync(p => p.Id == dto.PersonId);
            if (!personExists) throw new ArgumentException($"Person {dto.PersonId} not found.");

            var entity = new Order
            {
                PersonId = dto.PersonId,
                Number = dto.Number,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = 0,
                UpdatedAt = null
            };

            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Person)
                .Include(o => o.OrderDetails) // si quieres también los ítems: .ThenInclude(od => od.Item)
                .OrderBy(o => o.Id)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> UpdateOrderAsync(Order data)
        {
            var existing = await _context.Orders.FindAsync(data.Id);
            if (existing == null) return null;

            // Si permites cambio de Person:
            existing.PersonId = data.PersonId;

            existing.Number = data.Number;
            existing.UpdatedBy = data.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity == null) return false;

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
