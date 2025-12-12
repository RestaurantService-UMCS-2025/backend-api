using backend_api.Data;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly MenuService context;

    public MenuController(MenuService context)
    {
        this.context = context;
    }
    // api/Menu/all
    [HttpGet("all")]
    public List<Menu> GetAll()
    {
        return context.GetAll().Result;
    }
    // api/Menu/1
    [HttpGet("{id}")]
    public Task<Menu> GetById(int id)
    {
        return context.GetById(id);
    }
    // api/Menu/patch
    // {
    //     "id":1,
    //     "mode":false
    // }
    [HttpPatch("patch")]
    public void Patch([FromBody] PatchAvailableBody patchAvailableBody)
    {
        context.Patch(patchAvailableBody.id,patchAvailableBody.mode);
    }
}
public class PatchAvailableBody()
{
    public int id { get; set; }
    public bool mode { get; set; }
}