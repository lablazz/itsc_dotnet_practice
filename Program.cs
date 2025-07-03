using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Repositories;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Services;
using itsc_dotnet_practice.Services.Interface;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load .env in development/local environment
DotNetEnv.Env.Load();

// Prefer configuration from environment variables or appsettings, fallback to .env variables
string GetEnvOrDefault(string key, string defaultValue) =>
    Environment.GetEnvironmentVariable(key) ?? builder.Configuration[key] ?? defaultValue;

var dbHost = GetEnvOrDefault("DB_HOST", "localhost");
var dbPort = GetEnvOrDefault("DB_PORT", "5432");
var dbName = GetEnvOrDefault("DB_NAME", "itsc_db");
var dbUser = GetEnvOrDefault("DB_USER", "itsc_user");
var dbPassword = GetEnvOrDefault("DB_PASSWORD", "supersecurepassword");

var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
