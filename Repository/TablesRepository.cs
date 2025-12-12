using backend_api.Data;
using backend_api.Models;

namespace backend_api.Repository;

public class TablesRepository(ApiContext context)
{
    private readonly ApiContext context = context;

    public List<Table> GetAll()
    {
        return context.tables.ToList();
    }
    public Table GetById(int id)
    {
        return context.tables.FirstOrDefault(t => t.Id == id);
    }

    public List<Order> GetTableOrders(int id)
    {
        return context.orders.Where(o => o.TableId == id).ToList();
    }
    public void Save()
    {
        context.SaveChanges();
    }
}