using backend_api.Models;

namespace backend_api.Contracts;

public class PatchOrderStatusBody()
{
	// tutaj jakkolwiek ten enum będzie zaimplementowany
	public OrderStage Stage { get; set;}
}