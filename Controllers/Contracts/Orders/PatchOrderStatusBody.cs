using System.ComponentModel.DataAnnotations;
using backend_api.Models;

namespace backend_api.Contracts;

public class PatchOrderStatusBody()
{
	[Required(ErrorMessage = "No stage provided in body for PathOrderStatusBody")]
	public OrderStage? Stage { get; set;}
}