using backend_api.Contracts;
using backend_api.Models;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

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
    public async Task<ActionResult<List<Table>>> GetAll()
    {
        var data = await _tablesService.GetAll();
        return Ok(data);
    }
    [HttpGet("allAvailable")]
    public async Task<ActionResult<List<Table>>> GetAllFree()
    {
        var data = await _tablesService.GetAll();
        return Ok(data.Where(t=>t.Status != TableStatus.Paid));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Table>> GetById(int id)
    {
        var t = await _tablesService.GetById(id);
        if(t == null)
            return NotFound("Table not found");
        return Ok(t);
    }

    [Authorize(Roles = "User")]
    [HttpGet("{id}/orders")]
    public async Task<ActionResult<List<Order>>> GetTableOrders(int id)
    {
        var data = await _tablesService.GetTableOrders(id); 
        return Ok(data);
    }

    [Authorize(Roles = "User")]
    [HttpPatch("status")]
    public async Task<ActionResult> SetStatus([FromBody] TablesStatusRequest status)
    {
        var t = await _tablesService.SetTableStatus((int)status.id!, (string)status.status!);
        if(!t)
            return NotFound("Table not found");
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/clear")]
    public async Task<ActionResult> ClearTableInfo(int id)
    {
        var t = await _tablesService.ClearTable(id);
        if (!t)
            return NotFound("Table not found");
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("new")]
    public async Task<ActionResult<int>> CreateTable([FromBody] PostTableBody tableBody)
    {
        try
        {
            var o = await _tablesService.AddTableAsync(tableBody);
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

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/remove")]
    public async Task<ActionResult<int>> RemoveTable(int id)
    {
        var r = await _tablesService.SetTableStatus(id, "Paid");
        if (r)
            return Ok();
        return BadRequest("Table not found");
    }

    [HttpGet("{id}/qrcode")]
    public async Task<ActionResult<QRCodeData>> TableQrCode(int id)
    {
        var code = await _tablesService.TableQrCode(id);
        return code;
    }
}