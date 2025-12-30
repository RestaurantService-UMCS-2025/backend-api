using backend_api.Contracts;
using backend_api.Models;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;
[ApiController]
[Route("api/[controller]")]
public class OrdersController
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        this._ordersService = ordersService;
    }
	[HttpPost("order")]
    public int CreateOrder([FromBody] PostOrderBody orderBody)
    {
        return _ordersService.CreateNew(orderBody);
    }
    [HttpGet("orders")]
    public List<Order> GetAll()
    {
        return _ordersService.GetAll();
    }
    [HttpPost("orders")]
    public void AddToOrder(int orderId, List<OrderItems> orderItems)
    {
        _ordersService.AddToOrder(orderId, orderItems);
    }
    
    [HttpGet("orders/{id}")]
    public Order GetById(int id)
    {
        return _ordersService.GetById(id);
    }
    [HttpGet("orders/{id}/status")]
    public OrderStage GetStatusById(int id) 
    {
        return _ordersService.GetStatusById(id);
    }
    [HttpPatch("orders/{id}/status")]
    public void SetOrderStatusById(int id, [FromBody] PatchOrderStatusBody status)
    {
        _ordersService.SetOrderStatusById(id, status.Stage);
    }
    [HttpGet("orders/{id}/items")]
    public List<OrderItems> GetOrderItemsById(int id)
    {
        return _ordersService.GetOrderItemsById(id);
    }
}