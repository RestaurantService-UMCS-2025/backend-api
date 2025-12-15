using backend_api.Models;
using backend_api.Repository;

namespace backend_api.Services;

public class OrdersService(OrdersRepository context)
{
    private readonly OrdersRepository context = context;

    public void CreateNew()
    {
        // jakoś tu tworzymy

        context.Save();
    }
    public List<Order> GetAll()
    {
        return context.GetAll();
    }
    public Order GetById(int id)
    {
        return context.GetById(id);
    }
    public String GetStatusById(int id)
    {
        return context.GetById(id).Stage;
    }
    public void SetOrderStatusById(int id, String newStage)
    {
        context.GetById(id).Stage = newStage;
        context.Save();
    }
    public String GetOrderItemsById(int id)
    {
        return context.GetById(id).OrderData;
    }
    public void ArchiveByTableId(int id)
    {
        context.GetByTableId(id).Stage = "Paid";    // wedle dokumentu powinno być archived ale to najbliżej tego było z tych które mamy
    }
}