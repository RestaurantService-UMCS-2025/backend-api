using backend_api.Models;

namespace backend_api.Services.Interfaces;

public interface ITablesService
{
    public List<Table> GetAll();
    public Table? GetById(int id);
    public List<Order> GetTableOrders(int id);
    public bool SetTableStatus(int id, string status);
    public bool ClearTable(int id);
}