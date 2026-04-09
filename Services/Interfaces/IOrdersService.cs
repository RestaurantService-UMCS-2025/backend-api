using backend_api.Contracts;
using backend_api.Models;

namespace backend_api.Services.Interfaces;

public interface IOrdersService
{
    public int CreateNew(PostOrderBody orderBody);
    public Task<bool> AddToOrder(int orderId, List<OrderItems> orderItems);
    public Task<List<Order>> GetAll();
    public Task<Order?> GetById(int id);
    public Task<OrderStage> GetStatusById(int id);
    public Task<bool> SetOrderStatusById(int id, OrderStage newStage);
    public Task<List<OrderItems>> GetOrderItemsById(int id);
}