using backend_api.Data;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Repository.Interfaces;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend_api.Controllers.SignalR;

void AddScoped(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IMenuRepository, MenuRepository>();
    builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
    builder.Services.AddScoped<ITablesRepository, TablesRepository>();
    builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    
    builder.Services.AddScoped<IMenuService, MenuService>();
    builder.Services.AddScoped<IOrdersService, OrdersService>();
    builder.Services.AddScoped<ITablesService, TablesService>();
    builder.Services.AddScoped<IUsersService, UsersService>();
    
    builder.Services.AddScoped<JwtService>();
    builder.Services.AddScoped<PasswordHasher>();
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var keyString = builder.Configuration["Jwt:Key"];
var key = Encoding.UTF8.GetBytes(keyString);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApiContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    o =>
    {
        o.MapEnum<TableStatus>();
        o.MapEnum<OrderStage>();
        o.MapEnum<UserRole>();
    }));

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000", 
                "http://localhost:5173", 
                "http://localhost:5174",
                "http://192.168.1.12:5173",
                "http://20.100.201.238:3000"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen();

AddScoped(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<OrdersHub>("/ordersHub");
app.Run();