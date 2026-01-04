using System.ComponentModel.DataAnnotations;

namespace backend_api.Contracts;

public class TablesStatusRequest()
{
    [Required(ErrorMessage = "No id provided in body for SetTableStatusRequest")]
    public int? id { get; set; }
    [Required(ErrorMessage = "No status provided in body for SetTableStatusRequest")]
    public string? status { get; set; }
}