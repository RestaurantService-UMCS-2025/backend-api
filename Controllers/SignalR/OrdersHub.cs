using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace backend_api.Controllers.SignalR;


[AllowAnonymous]
public class OrdersHub : Hub
{
}