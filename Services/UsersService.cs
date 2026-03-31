using backend_api.Repository.Interfaces;
using backend_api.Services.Interfaces;

namespace backend_api.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository usersRepository;
    private readonly JwtService jwtService;
    private readonly PasswordHasher passwordHasher;

	public UsersService(IUsersRepository usersRepository, JwtService jwtService, PasswordHasher passwordHasher)
    {
        this.usersRepository = usersRepository;
        this.jwtService = jwtService;
        this.passwordHasher = passwordHasher;
    }

	public string Login(string login, string password)
	{
		var user = usersRepository.GetByLogin(login);
        
        if (user == null || !passwordHasher.Verify(password, user.Password)) return null;

        return jwtService.GenerateToken(user);
	}
}
