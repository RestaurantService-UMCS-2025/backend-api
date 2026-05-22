using System.ComponentModel.DataAnnotations;
using backend_api.Models;

namespace backend_api.Contracts;

public class PostTableBody()
{
    [Required(ErrorMessage = "No id provided in body for SetTableStatusRequest")]
    public int? id { get; set; }
    public string? tableInfo { get; set; }
}