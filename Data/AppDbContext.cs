using Microsoft.EntityFrameworkCore;
using MessageApi.Models;
using HelloApi.Models;

namespace MessageApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Message> Messages => Set<Message>();

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Detail> Details => Set<Detail>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    public DbSet<Item> Items => Set<Item>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Order>()
        .HasMany(t => t.OrderDetails)
        .WithOne(t => t.Order)
        .HasForeignKey(t => t.OrderId);

        modelBuilder.Entity<Item>()
        .HasMany(t => t.OrderDetails)
        .WithOne(t => t.Item)
        .HasForeignKey(t => t.ItemId);

        modelBuilder.Entity<Person>()
        .HasMany(t => t.Orders)
        .WithOne(t => t.Person)
        .HasForeignKey(t => t.PersonId);

    }


}
