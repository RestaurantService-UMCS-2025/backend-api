using backend_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;
[Route("menu")]
[ApiController]
public class MenuController(ApiContext context) : ControllerBase
{
    private ApiContext context = context;

    [HttpGet]
    public IActionResult GetAllMenu()
    {
        return Ok(context.menu.ToList());
    }
}