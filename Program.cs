using backend_api.Data;
using backend_api.Models;
using backend_api.Repository;
using backend_api.Repository.Interfaces;
using backend_api.Services;
using backend_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // Potrzebne do IgnoreCycles
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
        ValidateIssuer = false,     // might want to set it up if we choose so
        ValidateAudience = false,   // might want to set it up if we choose so
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

// // If you need a new password for a new user (its a pain in the ass to do it anywhere else)
// var hash = BCrypt.Net.BCrypt.HashPassword("surely_this_is_a_password");
// Console.WriteLine(hash);


builder.Services.AddDbContext<ApiContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    o =>
    {
        o.MapEnum<TableStatus>();
        o.MapEnum<OrderStage>();
        o.MapEnum<UserRole>();
    }));


//poprawki w cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // URL Twojego frontendu (np. Vite to 5173)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // TO JEST WYMAGANE DLA SIGNALR
    });
});

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen();

AddScoped(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//brakowało
app.UseCors("AllowFrontend"); 
app.UseCors("SignalRPolicy");
// app.UseHttpsRedirection(); // Zakomentowane - bardzo dobrze dla lokalnego HTTP

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<OrdersHub>("/ordersHub");
app.Run();