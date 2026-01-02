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
    public List<Table> GetAll()
    {
        return _tablesRepository.GetAll();
    }
    public Table  GetById(int id)
    {
        return _tablesRepository.GetById(id);
    }

    public List<Order> GetTableOrders(int id)
    {
        return _tablesRepository.GetTableOrders(id);
    }

    public bool SetTableStatus(int id, string status)
    {
        var table = _tablesRepository.GetById(id);
        if(table == null)
            return false;
        
        table.TableInfo = status;
        
        _tablesRepository.Save();
        return true;
    }

    public bool ClearTable(int id)
    {
        var table = _tablesRepository.GetById(id);
        if(table == null)
            return false;
        table.TableInfo = null;
        _tablesRepository.Save();
        return true;
    }
}