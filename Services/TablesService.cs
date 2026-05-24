using backend_api.Contracts;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Repository.Interfaces;
using backend_api.Services.Interfaces;
using QRCoder;

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
    
        table.Status = Enum.Parse<TableStatus>(status);
    
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

    public async Task<QRCodeData> TableQrCode(int id)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode($"http://20.100.201.238:3000/{id}",
            QRCodeGenerator.ECCLevel.Q);
        return qrCodeData;
    }

    
    public async Task<int> AddTableAsync(PostTableBody table)
    {
        var existingTables = await _tablesRepository.GetAll();
        var existingIds = existingTables.Select(x => x.Id).OrderBy(id => id).ToList();

        int targetId = 1;

        foreach (var id in existingIds)
        {
            if (id == targetId)
            {
                targetId++;
            }
            else if (id > targetId)
            {
                break;
            }
        }

        var t = new Table
        {
            Id = targetId,
            TableInfo = table.tableInfo,
            Status = TableStatus.Filed
        };
    
        _tablesRepository.Add(t);
        //await _tablesRepository.Save();
    
        return t.Id;
    }

    public void RemoveTable(int id)
    {
        throw new NotImplementedException();
    }
}