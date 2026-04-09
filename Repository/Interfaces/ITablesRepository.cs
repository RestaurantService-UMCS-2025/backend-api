using backend_api.Models;

namespace backend_api.Repository.Interfaces;

public interface ITablesRepository
{
    public Task<List<Table>> GetAll();
    public Task<Table?> GetById(int id);
    public Task<List<Order>> GetTableOrders(int id);
    public void Save();

}