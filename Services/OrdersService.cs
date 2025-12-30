using backend_api.Contracts;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Repository.Interfaces;
using backend_api.Services.Interfaces;

namespace backend_api.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository ordersRepository;

    public OrdersService(IOrdersRepository ordersRepository)
    {
        this.ordersRepository = ordersRepository;
    }
    public int CreateNew(PostOrderBody orderBody)
    {
        var o = new  Order
        {
            TableId = orderBody.tableId,
            Stage = OrderStage.NULL,
            BillAmount = 0,
        };
        ordersRepository.Add(o);

        return o.Id;
    }
    public void AddToOrder(int orderId, List<OrderItems> orderItems)
    {
        var order = ordersRepository.GetById(orderId);
        if (order == null)
            throw new Exception("Order not found");
        if(orderItems.Count == 0)
            throw new Exception("OrderItems not found");
        decimal sum = 0;
        foreach (var item in orderItems)
        {
            var newItem = new OrderItems
            {
                MenuItemId = item.MenuItemId,
                MenuItemName = item.MenuItemName,
                Quantity = item.Quantity,
                Note = item.Note,
                UnitPrice = item.UnitPrice,
            };
            sum += newItem.Quantity * item.UnitPrice;
            order.Items.Add(newItem);
        }
        SetOrderStatusById(orderId, OrderStage.Filled);
        UpdatePrice(orderId, sum);
        
        ordersRepository.Save();
    }

    private void UpdatePrice(int orderId, decimal newPrice)
    {
        var order = ordersRepository.GetById(orderId);
        if (order == null)
            throw new Exception("Order not found");
        order.BillAmount += newPrice;
        ordersRepository.Save();
    }
    public List<Order> GetAll()
    {
        return ordersRepository.GetAll();
    }
    public Order GetById(int id)
    {
        return ordersRepository.GetById(id);
    }
    public OrderStage GetStatusById(int id)
    {
        return ordersRepository.GetById(id).Stage;
    }
    public void SetOrderStatusById(int id, OrderStage newStage)
    {
        ordersRepository.GetById(id).Stage = newStage;
        ordersRepository.Save();
    }
    public List<OrderItems> GetOrderItemsById(int id)
    {
        return ordersRepository.GetById(id).Items;
    }
}