using backend_api.Models;

namespace backend_api.Services.Interfaces;

public interface ITablesService
{
    public List<Table> GetAll();
    public Table GetById(int id);
    public List<Order> GetTableOrders(int id);
    public void SetTableStatus(int id, string status);
    public void ClearTable(int id);
}