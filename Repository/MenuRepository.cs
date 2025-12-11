using backend_api.Data;
using backend_api.Models;

namespace backend_api.Repository;

public class MenuRepository
{
    private readonly ApiContext context;

    public MenuRepository(ApiContext context)
    {
        this.context = context;
    }

    public List<Menu> GetAll()
    {
        return context.menu.ToList();
    }

    public Menu GetById(int id)
    {
        var menu = context.menu.FirstOrDefault(menu => menu.Id == id);
        return menu;
    }

    public void Patch(int id,bool mode)
    {
        var menu = context.menu.FirstOrDefault(menu => menu.Id == id);
        menu.Available = mode;
        context.SaveChanges();
    }
}
