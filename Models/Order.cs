using System;
using System.Collections.Generic;

namespace backend_api.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? TableId { get; set; }

    public string? OrderData { get; set; }

    public decimal? BillAmount { get; set; }

    public string OrderStatus { get; set; }
    
    public virtual Table? Table { get; set; }
}
