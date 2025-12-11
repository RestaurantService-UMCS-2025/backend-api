using backend_api.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Data;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    public DbSet<Menu> menu { get; set; }
    public DbSet<Order> orders { get; set; }
    public DbSet<Table> tables { get; set; }
    public DbSet<User> users { get; set; }
}