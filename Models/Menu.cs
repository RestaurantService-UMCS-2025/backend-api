using System;
using System.Collections.Generic;

namespace backend_api.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string DishName { get; set; }

    public decimal Price { get; set; }

    public bool Available { get; set; }
}
