using System.Text;
using backend_api.Data;
using backend_api.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<ApiContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();

app.UseHttpsRedirection();
var endPoints  = app.Services.GetServices<EndpointDataSource>().SelectMany(x=>x.Endpoints).ToList();
Console.WriteLine(endPoints.Count);
Console.WriteLine(endPoints);
app.Run();