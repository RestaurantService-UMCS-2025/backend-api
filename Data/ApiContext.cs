using backend_api.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Data;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // jeśli potrzebujesz jawnie wskazać klucz:
        modelBuilder.Entity<OrderItems>()
            .HasKey(oi => oi.OrderItemId);

        // inne konfiguracje
    }
    public DbSet<Menu> menu { get; set; }
    public DbSet<Order> orders { get; set; }
    public DbSet<Table> tables { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<OrderItems>  orderItems { get; set; }
}