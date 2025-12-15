using backend_api.Data;
using backend_api.Repository;
using backend_api.Services;
using Microsoft.EntityFrameworkCore;

void AddScoped(WebApplicationBuilder builder)
{
    builder.Services.AddScoped(typeof(MenuRepository));
    builder.Services.AddScoped(typeof(MenuService));
    builder.Services.AddScoped(typeof(TablesRepository));
    builder.Services.AddScoped(typeof(TablesService));
    builder.Services.AddScoped(typeof(OrdersRepository));
    builder.Services.AddScoped(typeof(OrdersService));
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

AddScoped(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.UseHttpsRedirection();

app.Run(); 

