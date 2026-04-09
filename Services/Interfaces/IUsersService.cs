namespace backend_api.Services.Interfaces;

public interface IUsersService	// basically the authentication service but still
{
	public Task<string?> Login(string login, string password);
}