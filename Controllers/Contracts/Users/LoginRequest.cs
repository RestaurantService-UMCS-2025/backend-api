using System.ComponentModel.DataAnnotations;

namespace backend_api.Contracts;

public class LoginRequest()
{
    [Required(ErrorMessage = "No login provided in body for LoginRequest")]
    public string? Login { get; set; }
    [Required(ErrorMessage = "No password provided in body for LoginRequest")]
    public string? Password { get; set; }
}