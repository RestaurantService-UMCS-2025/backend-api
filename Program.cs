using backend_api.Data;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Repository.Interfaces;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

void AddScoped(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IMenuRepository, MenuRepository>();
    builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
    builder.Services.AddScoped<ITablesRepository, TablesRepository>();
    
    builder.Services.AddScoped<IMenuService, MenuService>();
    builder.Services.AddScoped<IOrdersService, OrdersService>();
    builder.Services.AddScoped<ITablesService, TablesService>();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    o =>
    {
        o.MapEnum<TableStatus>();
        o.MapEnum<OrderStage>();
        o.MapEnum<UserRole>();
    }));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173")

                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
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

