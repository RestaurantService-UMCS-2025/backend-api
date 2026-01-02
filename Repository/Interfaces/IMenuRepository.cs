using backend_api.Models;

namespace backend_api.Repository.Interfaces;

public interface IMenuRepository
{
    public List<Menu> GetAll();
    public Menu? GetById(int id);
    public void Add(Menu menu);
    public void Save();
}