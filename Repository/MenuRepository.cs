using backend_api.Data;
using backend_api.Models;
using backend_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly ApiContext context;

    public MenuRepository(ApiContext context)
    {
        this.context = context;
    }

    public async Task<List<Menu>> GetAll()
    {
        return await context.menu.ToListAsync();
    }

    public async Task<Menu?> GetById(int id)
    {
        return await context.menu.FirstOrDefaultAsync(menu => menu.Id == id);
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
