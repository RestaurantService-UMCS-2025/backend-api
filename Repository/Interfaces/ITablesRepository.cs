using backend_api.Models;

namespace backend_api.Repository.Interfaces;

public interface ITablesRepository
{
    public List<Table> GetAll();
    public Table GetById(int id);
    public List<Order> GetTableOrders(int id);
    public void Save();

}