using backend_api.Models;

namespace backend_api.Repository.Interfaces;

public interface IOrdersRepository
{
    public List<Order> GetAll();
    public Order? GetById(int id);
    public void Add(Order order);
    public void Save();
}