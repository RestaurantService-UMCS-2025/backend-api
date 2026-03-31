namespace backend_api.Services.Interfaces;

public interface IUsersService	// basically the authentication service but still
{
	public string Login(string login, string password);
}