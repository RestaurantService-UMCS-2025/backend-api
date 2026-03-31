using System;
using System.Collections.Generic;

namespace backend_api.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;   // i hope to god that this is going to be hashed
    
    public UserRole Role { get; set; }
}
