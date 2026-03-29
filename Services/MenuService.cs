using backend_api.Models;
using backend_api.Repository;
using backend_api.Repository.Interfaces;
using backend_api.Services.Interfaces;

namespace backend_api.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        this.menuRepository = menuRepository;
    }

    public Task<List<Menu>> GetAll()
    {
        return Task.FromResult(menuRepository.GetAll().OrderBy(entry => entry.DishName).ToList());
    }

    public List<Menu> GetAvailable()
    {
        return menuRepository.GetAll().Where(o => o.Available == true).OrderBy(entry => entry.DishName).ToList();
    }
    public Menu GetById(int id)
    {
        var menu = menuRepository.GetById(id);
        return menuRepository.GetById(id);
    }

    public bool SetAvailable(int id,bool mode)
    {
        var menu = menuRepository.GetById(id);
        if (menu == null)
        {
            return false;
        }
        menu.Available = mode;
        menuRepository.Save();
        return true;
    }

    public void SetPlaceholderData()
    {
        var menuItems = new List<Menu>
        {
            new Menu { DishName = "Spaghetti Carbonara", Price = 12.99m , Available = true},
            new Menu { DishName = "Margherita Pizza", Price = 8.49m , Available = true},
            new Menu { DishName = "Caesar Salad", Price = 5.99m , Available = true},
            new Menu { DishName = "Grilled Salmon", Price = 15.99m , Available = false},
            new Menu { DishName = "Beef Tacos", Price = 9.49m , Available = true},
            new Menu { DishName = "Baked Chicken Alfredo", Price = 10.99m , Available = false},
            new Menu { DishName = "Vegetable Stir-Fry", Price = 7.99m , Available = true},
            new Menu { DishName = "Chicken Parmesan", Price = 13.49m , Available = true},
            new Menu { DishName = "Avocado Toast", Price = 5.49m , Available = false},
            new Menu { DishName = "Caesar Salad with Shrimp", Price = 12.99m , Available = true}
        };
        foreach (var menu in menuItems)
        {
            menuRepository.Add(menu);
        }
    }
}