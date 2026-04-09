using backend_api.Data;
using backend_api.Models;
using backend_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Repository;

public class UsersRepository : IUsersRepository
{
	private readonly ApiContext context;

	public UsersRepository(ApiContext context)
	{
		this.context = context;
	}

	public async Task<User?> GetByLogin(string login)
	{
		return await context.users.FirstOrDefaultAsync(user => user.Login.Equals(login));
	}
}