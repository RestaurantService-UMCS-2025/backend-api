using backend_api.Models;

namespace backend_api.Repository.Interfaces;

public interface IUsersRepository
{
	// public List<User> GetAll();
	// public User? GetById(int id);
	public User? GetByLogin(string login);
	// public List<User> GetByRole(UserRole userRole);
	

	// Should this handle adding new entries?
	// Maybe add some removal?
	// What about the roles? -> should they be changable or not?
}
