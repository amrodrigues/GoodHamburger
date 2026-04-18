using GoodHamburger.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Api.Data;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

    public DbSet<MenuItem> MenuItems { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MenuItem>().HasKey(m => m.Id);

        // Seed initial menu
        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 1, Name = "X Burger", Price = 5.00m, Type = ItemType.Sandwich },
            new MenuItem { Id = 2, Name = "X Egg", Price = 4.50m, Type = ItemType.Sandwich },
            new MenuItem { Id = 3, Name = "X Bacon", Price = 7.00m, Type = ItemType.Sandwich },
            new MenuItem { Id = 4, Name = "Batata frita", Price = 2.00m, Type = ItemType.Side },
            new MenuItem { Id = 5, Name = "Refrigerante", Price = 2.50m, Type = ItemType.Drink }
        );

        modelBuilder.Entity<Order>(eb =>
        {
            eb.HasKey(o => o.Id);
            eb.Property(o => o.ItemIds)
              .HasConversion(
                  v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                  v => System.Text.Json.JsonSerializer.Deserialize<List<int>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<int>());
        });
    }
}
