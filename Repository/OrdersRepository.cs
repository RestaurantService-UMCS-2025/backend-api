using backend_api.Data;
using backend_api.Models;
using backend_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Repository;

public class OrdersRepository : IOrdersRepository
{
    private readonly ApiContext context;

    public OrdersRepository(ApiContext context)
    {
        this.context = context;
    }
    public async Task<List<Order>> GetAll()
    {
        return await context.orders
            .Include(o => o.Items)
            .ToListAsync();
    }
    public async Task<Order?> GetById(int id)
    {
        return await context.orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public void Add(Order order)
    {
        context.orders.Add(order);
        context.SaveChanges();
    }
    public void Save()
    {
        Console.WriteLine("Saving Orders");
        context.SaveChanges();
    }
}