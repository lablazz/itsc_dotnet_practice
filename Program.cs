using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Seeds;  // For DatabaseSeeder
using itsc_dotnet_practice.Services; // For UserService
using itsc_dotnet_practice.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Load .env so environment variables like AES_KEY and AES_IV are set
DotNetEnv.Env.Load();

// Helper to load from ENV or fallback to appsettings
string GetEnvOrDefault(string key, string defaultValue)
{
    var envValue = Environment.GetEnvironmentVariable(key);
    if (!string.IsNullOrEmpty(envValue))
        return envValue;

    var configValue = builder.Configuration[key];
    if (!string.IsNullOrEmpty(configValue))
        return configValue;

    return defaultValue;
}

// DB Connection values
var dbHost = GetEnvOrDefault("DB_HOST", "localhost");
var dbPort = GetEnvOrDefault("DB_PORT", "5432");
var dbName = GetEnvOrDefault("DB_NAME", "itsc_db");
var dbUser = GetEnvOrDefault("DB_USER", "itsc_user");
var dbPassword = GetEnvOrDefault("DB_PASSWORD", "supersecurepassword");

var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ⛓ Register services and repositories by convention
builder.Services.AddScopedServicesByConvention(Assembly.GetExecutingAssembly());

// Make sure UserService is registered (if not registered by convention, register here explicitly)
builder.Services.AddScoped<UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userService = scope.ServiceProvider.GetRequiredService<UserService>();

    await dbContext.Database.MigrateAsync();

    // Await seed async and pass UserService
    await DatabaseSeeder.SeedAsync(dbContext, userService);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
