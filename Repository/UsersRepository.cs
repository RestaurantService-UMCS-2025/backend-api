using backend_api.Data;
using backend_api.Models;
using backend_api.Repository.Interfaces;

namespace backend_api.Repository;

public class UsersRepository : IUsersRepository
{
	private readonly ApiContext context;

	public UsersRepository(ApiContext context)
	{
		this.context = context;
	}

	public User? GetByLogin(string login)
	{
		var user = context.users.FirstOrDefault(user => user.Login.Equals(login));
		return user;
	}
}