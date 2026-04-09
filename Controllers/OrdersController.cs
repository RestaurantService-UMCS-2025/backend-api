using backend_api.Contracts;
using backend_api.Models;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        this._ordersService = ordersService;
    }

    // [Authorize(Roles = "User")]
    // tu też na chwilę obecną zostawiam zakomentowane, nie jestem pewien czy to obsługa zamawia
    // czy to ci co przyszli zamawiają ze swojego telefonu
	[HttpPost("order")]
    public ActionResult<int> CreateOrder([FromBody] PostOrderBody orderBody)
    {
        try
        {
            var o = _ordersService.CreateNew(orderBody);
            if (o != -1)
            {
                return Ok(o);
            }
        }
        catch(Exception e)
        {
            return BadRequest(e);
        }

        return -1;
    }

    [Authorize(Roles = "User")]
    [HttpGet("orders")]
    public ActionResult<List<Order>> GetAll()
    {
        return Ok(_ordersService.GetAll());
    }

    // [Authorize(Roles = "User")]
    // zakomentowane z tego samego powodu co powyżej
    [HttpPost("items")]
    public ActionResult AddToOrder(int orderId, List<OrderItems> orderItems)
    {
        var o = _ordersService.AddToOrder(orderId, orderItems);
        if (o)
        {
            return Ok();
        }
        return BadRequest("Can't add items to order");
    }
    
    [Authorize(Roles = "User")]
    [HttpGet("orders/{id}")]
    public ActionResult<Order> GetById(int id)
    {
        var o  = _ordersService.GetById(id);
        if (o == null)
        {
            return NotFound();
        }
        return Ok(_ordersService.GetById(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("orders/{id}/status")]
    public OrderStage GetStatusById(int id) 
    {
        return _ordersService.GetStatusById(id);
    }

    [Authorize(Roles = "User")]
    [HttpPatch("orders/{id}/status")]
    public ActionResult SetOrderStatusById(int id, [FromBody] PatchOrderStatusBody status)
    {
        var o = _ordersService.SetOrderStatusById(id, (OrderStage)status.Stage!);
        if(o)
            return Ok();
        return BadRequest();
    }

    // [Authorize(Roles = "User")]
    // tak jak powyżej
    [HttpGet("orders/{id}/items")]
    public ActionResult<List<OrderItems>> GetOrderItemsById(int id)
    {
        return Ok(_ordersService.GetOrderItemsById(id));
    }
}