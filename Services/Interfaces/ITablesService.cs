using backend_api.Contracts;
using backend_api.Models;
using QRCoder;

namespace backend_api.Services.Interfaces;

public interface ITablesService
{
    public Task<List<Table>> GetAll();
    public Task<Table?> GetById(int id);
    public Task<List<Order>> GetTableOrders(int id);
    public Task<bool> SetTableStatus(int id, string status);
    public Task<bool> ClearTable(int id);
    public int AddTable(PostTableBody table);
    public void RemoveTable(int id);
    public Task<QRCodeData> TableQrCode(int id);
}