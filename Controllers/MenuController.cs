using backend_api.Contracts.Menu;
using backend_api.Data;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;

[ApiController]
[Route("api/[controller]")]
public class MenuController: ControllerBase
{
    private readonly IMenuService menuService;

    public MenuController(IMenuService menuService)
    {
        this.menuService = menuService;
    }
    [HttpGet("all")]
    public ActionResult<List<Menu>> GetAll()
    {
        return Ok(menuService.GetAll().Result);
    }
    
    [HttpGet("byId")]
    public ActionResult<Menu> GetById(int id)
    {
        var menu = menuService.GetById(id);
        if (menu == null)
        {
            return NotFound();
        }
        return Ok(menuService.GetById(id));
    }
    
    [HttpPatch("available")]
    public ActionResult SetAvailable([FromBody] PatchAvailableBody patchAvailableBody)
    {
        var result = menuService.SetAvailable((int)patchAvailableBody.id!, (bool)patchAvailableBody.mode!);
        if (result == false)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpPut("placeholderData")]
    public void SetPlaceholderData()
    {
        menuService.SetPlaceholderData();
    }

    [HttpGet("availableMenu")]
    public List<Menu> GetAvailable()
    {
        return menuService.GetAvailable();
    }
}