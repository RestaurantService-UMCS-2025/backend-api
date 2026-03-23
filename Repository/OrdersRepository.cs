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
    public List<Order> GetAll()
    {
        return context.orders
            .Include(o => o.Items)
            .ToList();
    }
    public Order? GetById(int id)
    {
        return context.orders
            .Include(o => o.Items)
            .FirstOrDefault(t => t.Id == id);
    }

    public void Add(Order order)
    {
        context.orders.Add(order);
        context.SaveChanges();
    }
    public void Save()
    {
        context.SaveChanges();
    }
}