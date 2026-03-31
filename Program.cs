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
    options.AddPolicy("AllowFrontend", //Nie AllowReactApp
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173","http://localhost:5174")
                .AllowAnyHeader()
                .AllowAnyMethod();
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

// app.UseHttpsRedirection(); // Zakomentowane - bardzo dobrze dla lokalnego HTTP

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();