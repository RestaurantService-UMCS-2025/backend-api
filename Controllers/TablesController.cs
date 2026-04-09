using backend_api.Contracts;
using backend_api.Models;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;
[ApiController]
[Route("api/[controller]")]
public class TablesController :  ControllerBase
{
    private readonly ITablesService  _tablesService;

    public TablesController(ITablesService tablesService)
    {
        _tablesService = tablesService;
    }


    [HttpGet("all")]
    public ActionResult<List<Table>> GetAll()
    {
        return Ok(_tablesService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Table> GetById(int id)
    {
        var t =  _tablesService.GetById(id);
        if(t == null)
            return NotFound("Table not found");
        return Ok(t);
    }

    [Authorize(Roles = "User")]
    [HttpGet("{id}/orders")]
    public ActionResult<List<Order>> GetTableOrders(int id)
    {
        return Ok(_tablesService.GetTableOrders(id));
    }

    [Authorize(Roles = "User")]
    [HttpPatch("status")]
    public ActionResult SetStatus([FromBody] TablesStatusRequest status)
    {
        var t = _tablesService.SetTableStatus((int)status.id!, (string)status.status!);
        if(!t)
            return NotFound("Table not found");
        return Ok();
    }

    [Authorize(Roles = "User")]
    [HttpPatch("{id}/clear")]
    public ActionResult ClearTableInfo(int id)
    {
        var t = _tablesService.ClearTable(id);
        if (!t)
            return NotFound("Table not found");
        return Ok();
    }
}