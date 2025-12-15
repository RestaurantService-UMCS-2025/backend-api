using backend_api.Contracts;
using backend_api.Models;
using backend_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;
[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrdersService context)
{
    private readonly OrdersService  context = context;

	// [HttpPost("order")] // NIE JEST ZAIMPLEMENTOWANE POPRAWNIE
    // public void CreateOrder([FromBody] PostOrderBody status)
    // {
    //     context.CreateNew();
    // }
    [HttpGet("orders")]
    public List<Order> GetAll()
    {
        return context.GetAll();
    }
    [HttpGet("orders/{id}")]
    public Order GetById(int id)
    {
        return context.GetById(id);
    }
    [HttpGet("orders/{id}/status")]
    public String GetStatusById(int id) 
    {
        return context.GetStatusById(id);
    }
    [HttpPatch("orders/{id}/status")]
    public void SetOrderStatusById(int id, [FromBody] PatchOrderStatusBody status)
    {
        context.SetOrderStatusById(id, status.Stage);
    }
    [HttpGet("orders/{id}/items")]
    public String GetOrderItemsById(int id)
    {
        return context.GetOrderItemsById(id);
    }
    [HttpPatch("orders/{id}/clear")]
    public void SetOrderAsArchivedById(int id)
    {
        context.ArchiveByTableId(id);
    }
}