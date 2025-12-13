namespace backend_api.Models;

public enum OrderStage
{
    Filled,
    AwaitingPayment,
    Paid,
    NULL
}