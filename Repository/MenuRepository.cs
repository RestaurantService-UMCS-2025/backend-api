using backend_api.Data;
using backend_api.Models;
using backend_api.Repository.Interfaces;

namespace backend_api.Repository;

public class MenuRepository : IMenuRepository
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

    public void Add(Menu menu)
    {
        context.menu.Add(menu);
        context.SaveChanges();
    }
    public void Save()
    {
        context.SaveChanges();
    }
}
