namespace backend_api.Models;

public class OrderItems
{
    public int OrderItemId { get; set; }
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Note { get; set; }
    public OrderItemStatus Status { get; set; }
}