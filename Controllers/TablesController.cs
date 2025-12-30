using backend_api.Contracts;
using backend_api.Models;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;
[ApiController]
[Route("api/[controller]")]
public class TablesController
{
    private readonly ITablesService  _tablesService;

    public TablesController(ITablesService tablesService)
    {
        _tablesService = tablesService;
    }
    [HttpGet("all")]
    public List<Table> GetAll()
    {
        return _tablesService.GetAll();
    }
    [HttpGet("{id}")]
    public Table GetById(int id)
    {
        return _tablesService.GetById(id);
    }
    [HttpGet("{id}/orders")]
    public List<Order> GetTableOrders(int id)
    {
        return _tablesService.GetTableOrders(id);
    }

    [HttpPatch("status")]
    public void SetStatus([FromBody] TablesStatusRequest status)
    {
        _tablesService.SetTableStatus(status.id, status.status);
    }

    [HttpPatch("{id}/clear")]
    public void ClearTableInfo(int id)
    {
        _tablesService.ClearTable(id);
    }
}