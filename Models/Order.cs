using System;
using System.Collections.Generic;

namespace backend_api.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? TableId { get; set; }

    public string? Order1 { get; set; }

    public decimal? BillAmount { get; set; }

    public virtual Table? Table { get; set; }
}
