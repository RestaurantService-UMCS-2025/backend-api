using backend_api.Contracts;
using backend_api.Services.Interfaces;
using backend_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_api;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
	private readonly IUsersService usersService;

	public UsersController(IUsersService usersService)
	{
		this.usersService = usersService;
	}

	[HttpPost("login")]
	public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
	{
		var token = await usersService.Login(loginRequest.Login!, loginRequest.Password!);

		if (token == null) return Unauthorized("Invalid credentials");

		return Ok(new { Token = token });
	}

	[HttpGet("public")]
	public ActionResult Public()
	{
		return Ok("Yooo this is public, everyone SHOULD see this one");
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("private")]
	public ActionResult Private()
	{
		return Ok("Only you should be able to see this one... >_>");
	}

	[Authorize(Roles = "User")]
	[HttpGet("userCheck")]
	public ActionResult UserCheck()
	{
		return Ok("Just put the fires in the bag lil bro");
	}
}