using backend_api.Models;

namespace backend_api.Services.Interfaces;

public interface IMenuService
{
    public Task<List<Menu>> GetAll();
    public List<Menu> GetAvailable();
    public Task<Menu> GetById(int id);
    public void SetAvailable(int id, bool mode);
    void SetPlaceholderData();
}