using backend_api.Models;
using backend_api.Repository;

namespace backend_api.Services;

public class TablesService(TablesRepository context)
{
    private readonly TablesRepository context = context;

    public List<Table> GetAll()
    {
        return context.GetAll();
    }
    public Table  GetById(int id)
    {
        return context.GetById(id);
    }

    public List<Order> GetTableOrders(int id)
    {
        return context.GetTableOrders(id);
    }

    public void SetTableStatus(int id, string status)
    {
        var table = context.GetById(id);
        table.TableInfo = status;
        
        context.Save();
    }
}