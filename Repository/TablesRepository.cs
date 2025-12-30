using backend_api.Data;
using backend_api.Models;
using backend_api.Repository.Interfaces;

namespace backend_api.Repository;

public class TablesRepository :  ITablesRepository
{ 
    private readonly ApiContext context;

    public TablesRepository(ApiContext context)
    {
        this.context = context;
    }
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