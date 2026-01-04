using System.ComponentModel.DataAnnotations;
using backend_api.Models;

namespace backend_api.Contracts;

public class PostOrderBody()
{
	[Required(ErrorMessage = "No tableId provided in body for PostOrderBody")]
	public int? tableId { get; set; }
}