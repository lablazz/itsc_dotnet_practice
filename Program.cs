using DotNetEnv;
using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Repositories;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services;
using itsc_dotnet_practice.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load .env variables
Env.Load();

// Read DB & JWT values
var dbHost = Env.GetString("DB_HOST");
var dbPort = Env.GetString("DB_PORT");
var dbName = Env.GetString("DB_NAME");
var dbUser = Env.GetString("DB_USER");
var dbPass = Env.GetString("DB_PASSWORD");

var jwtKey = Env.GetString("JWT_KEY");
var jwtIssuer = Env.GetString("JWT_ISSUER");
var jwtAudience = Env.GetString("JWT_AUDIENCE");

var connBuilder = new Npgsql.NpgsqlConnectionStringBuilder()
{
    Host = dbHost,
    Port = int.Parse(dbPort),
    Database = dbName,
    Username = dbUser,
    Password = dbPass
};

string connectionString = connBuilder.ConnectionString;

Console.WriteLine($"Using DB host: {dbHost}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
