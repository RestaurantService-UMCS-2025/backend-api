using backend_api.Models;
using backend_api.Repository;

namespace backend_api.Services;

public class MenuService
{
    private readonly MenuRepository context;

    public MenuService(MenuRepository context)
    {
        this.context = context;
    }

    public Task<List<Menu>> GetAll()
    {
        return Task.FromResult(context.GetAll());
    }

    public async Task<Menu> GetById(int id)
    {
        return context.GetById(id);
    }

    public void Patch(int id,bool mode)
    {
        context.Patch(id,mode);
    }
}