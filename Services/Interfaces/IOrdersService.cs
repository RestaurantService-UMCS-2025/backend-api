using backend_api.Contracts;
using backend_api.Models;

namespace backend_api.Services.Interfaces;

public interface IOrdersService
{
    public int CreateNew(PostOrderBody orderBody);
    public void AddToOrder(int orderId, List<OrderItems> orderItems);
    public List<Order> GetAll();
    public Order GetById(int id);
    public OrderStage GetStatusById(int id);
    public void SetOrderStatusById(int id, OrderStage newStage);
    public List<OrderItems> GetOrderItemsById(int id);
}