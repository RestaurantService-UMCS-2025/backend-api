using System;
using System.Collections.Generic;

namespace backend_api.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? TableId { get; set; }

    public List<OrderItems> Items { get; set; } = new List<OrderItems>();

    public decimal? BillAmount { get; set; }

    public OrderStage Stage { get; set; }
    
    public virtual Table? Table { get; set; }
}
