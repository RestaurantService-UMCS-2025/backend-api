using System.ComponentModel.DataAnnotations;

namespace backend_api.Contracts.Menu;

public class PatchAvailableBody()
{
    [Required(ErrorMessage = "No id provided in body for PatchAvailableBody")]
    public int? id { get; set; }
    [Required(ErrorMessage = "No mode provided in body for PatchAvailableBody")]
    public bool? mode { get; set; }
}