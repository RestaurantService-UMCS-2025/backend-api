namespace backend_api.Contracts;

public class PostOrderBody()
{
	public int id { get; set; }
	public int tableId { get; set; }
    public string orderData { get; set; }
	public decimal billAmount { get; set; }
	// nie jestem pewien co do tego jak stage tu ogarnąć
	// jak i również table
}