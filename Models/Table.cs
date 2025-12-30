using System;
using System.Collections.Generic;

namespace backend_api.Models;

public partial class Table
{
    public int Id { get; set; }

    public string? TableInfo { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public TableStatus Status { get; set; } 
}
