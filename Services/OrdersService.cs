using backend_api.Contracts;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Repository.Interfaces;
using backend_api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

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
    public async Task<bool> AddToOrder(int orderId, List<OrderItems> orderItems)
    {
        var order = await ordersRepository.GetById(orderId);
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
        //await SetOrderStatusById(orderId, OrderStage.Filled);
        //UpdatePrice(orderId, sum);
        
        ordersRepository.Save();
        return true;
    }

    private async void UpdatePrice(int orderId, decimal newPrice)
    {
        var order = await ordersRepository.GetById(orderId);
        if (order == null)
            throw new Exception("Order not found");
        order.BillAmount += newPrice;
        ordersRepository.Save();
    }
    public async Task<List<Order>> GetAll()
    {
        return await ordersRepository.GetAll();
    }
    public async Task<Order?> GetById(int id)
    {
        return await ordersRepository.GetById(id);
    }
    public async Task<OrderStage> GetStatusById(int id)
    {
        var data = await ordersRepository.GetById(id); 
        return data.Stage;
    }
    public async Task<bool> SetOrderStatusById(int id, OrderStage newStage)
    {
        var o = await ordersRepository.GetById(id);
        if (o == null)
            return false;
        o.Stage = newStage;
        ordersRepository.Save();
        return true;
    }
    public async Task<List<OrderItems>> GetOrderItemsById(int id)
    {
        var data = await ordersRepository.GetById(id);
        return data.Items;
    }
}