using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_api.Models;
using backend_api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

public class JwtService
{
	private readonly string key;	// if need be we set it to nullable

	public JwtService(IConfiguration configuration)
	{
		key = configuration["Jwt:Key"];
	}

	public string GenerateToken(User user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Email, user.Login),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		};

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
		var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.UtcNow.AddHours(2),
			signingCredentials: creds
		);	

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}