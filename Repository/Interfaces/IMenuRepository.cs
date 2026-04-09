using backend_api.Models;

namespace backend_api.Repository.Interfaces;

public interface IMenuRepository
{
    public Task<List<Menu>> GetAll();
    public Task<Menu?> GetById(int id);
    public void Add(Menu menu);
    public void Save();
}