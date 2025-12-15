using backend_api.Data;
using backend_api.Models;

namespace backend_api.Repository;

public class OrdersRepository(ApiContext context)
{
    private readonly ApiContext context = context;

    public List<Order> GetAll()
    {
        return context.orders.ToList();
    }
    public Order GetById(int id)
    {
        return context.orders.FirstOrDefault(t => t.Id == id);
    }
    public Order GetByTableId(int id)
    {
        return null;    // nie jestem pewien jak to zrobić
    }
    public void Save()
    {
        context.SaveChanges();
    }
}