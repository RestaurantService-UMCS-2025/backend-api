using backend_api.Models;
using backend_api.Repository;
using backend_api.Repository.Interfaces;
using backend_api.Services.Interfaces;

namespace backend_api.Services;

public class TablesService : ITablesService
{
    private readonly ITablesRepository _tablesRepository;
    public TablesService(ITablesRepository tablesRepository)
    {
        this._tablesRepository = tablesRepository;
    }
    public async Task<List<Table>> GetAll()
    {
        return await _tablesRepository.GetAll();
    }
    public async Task<Table?> GetById(int id)
    {
        return await _tablesRepository.GetById(id);
    }

    public async Task<List<Order>> GetTableOrders(int id)
    {
        return await _tablesRepository.GetTableOrders(id);
    }

    public async Task<bool> SetTableStatus(int id, string status)
    {
        var table = await _tablesRepository.GetById(id);
        if(table == null)
            return false;
        
        table.TableInfo = status;
        
        _tablesRepository.Save();
        return true;
    }

    public async Task<bool> ClearTable(int id)
    {
        var table = await _tablesRepository.GetById(id);
        if(table == null)
            return false;
        table.TableInfo = null;
        _tablesRepository.Save();
        return true;
    }
}