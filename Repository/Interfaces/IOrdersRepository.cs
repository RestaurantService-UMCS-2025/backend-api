using backend_api.Models;

namespace backend_api.Repository.Interfaces;

public interface IOrdersRepository
{
    public Task<List<Order>> GetAll();
    public Task<Order?> GetById(int id);
    public void Add(Order order);
    public void Save();
}