using backend_api.Contracts;
using backend_api.Models;
using backend_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;
[ApiController]
[Route("api/[controller]")]
public class TablesController(TablesService context)
{
    private readonly TablesService  context = context;

    [HttpGet("all")]
    public List<Table> GetAll()
    {
        return context.GetAll();
    }
    [HttpGet("{id}")]
    public Table GetById(int id)
    {
        return context.GetById(id);
    }
    [HttpGet("{id}/orders")]
    public List<Order> GetTableOrders(int id)
    {
        return context.GetTableOrders(id);
    }

    [HttpPatch("setStatus")]
    public void SetStatus([FromBody] TablesStatusRequest status)
    {
        context.SetTableStatus(status.id, status.status);
    }

    [HttpPatch("{id}/clear")]
    public void ClearTableInfo(int id)
    {
        context.ClearTable(id);
    }
}