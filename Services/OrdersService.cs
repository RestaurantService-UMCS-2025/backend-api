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
        ordersRepository.Save();
        return o.Id;
    }
    public bool AddToOrder(int orderId, List<OrderItems> orderItems)
    {
        var order = ordersRepository.GetById(orderId);
        if (order == null)
            return false;
        if (orderItems.Count == 0)
            return false;
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
        return true;
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
    public bool SetOrderStatusById(int id, OrderStage newStage)
    {
        var o = ordersRepository.GetById(id);
        if (o == null)
            return false;
        o.Stage = newStage;
        ordersRepository.Save();
        return true;
    }
    public List<OrderItems> GetOrderItemsById(int id)
    {
        return ordersRepository.GetById(id).Items;
    }
}