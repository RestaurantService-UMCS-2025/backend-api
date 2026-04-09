using backend_api.Data;
using backend_api.Models;
using backend_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Repository;

public class TablesRepository :  ITablesRepository
{ 
    private readonly ApiContext context;

    public TablesRepository(ApiContext context)
    {
        this.context = context;
    }
    public async Task<List<Table>> GetAll()
    {
        return await context.tables.ToListAsync();
    }
    public async Task<Table?> GetById(int id)
    {
        return await context.tables.FirstOrDefaultAsync(t => t.Id == id);
    }
    public async Task<List<Order>> GetTableOrders(int id)
    {
        return await context.orders.Where(o => o.TableId == id).ToListAsync();
    }
    public void Save()
    {
        context.SaveChanges();
    }
}