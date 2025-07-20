using DotNetEnv;
using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Document;
using itsc_dotnet_practice.Document.Interface;
using itsc_dotnet_practice.Repositories;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Seeds;
using itsc_dotnet_practice.Services;
using itsc_dotnet_practice.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load .env variables
Env.Load();

// Read DB & JWT values with defaults to avoid errors
var dbHost = Env.GetString("DB_HOST") ?? "localhost";
var dbPort = Env.GetString("DB_PORT") ?? "5432";
var dbName = Env.GetString("DB_NAME") ?? "your_db";
var dbUser = Env.GetString("DB_USER") ?? "user";
var dbPass = Env.GetString("DB_PASSWORD") ?? "pass";

var jwtKey = Env.GetString("JWT_KEY") ?? "your_jwt_secret_key";
var jwtIssuer = Env.GetString("JWT_ISSUER") ?? "your_issuer";
var jwtAudience = Env.GetString("JWT_AUDIENCE") ?? "your_audience";

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

// Register DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register repositories and services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Register your custom Document service for Swagger
builder.Services.AddSingleton<IDocument, Document>();

// Configure JWT Bearer Authentication
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

// Authorization & Controllers
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with your Document class providing security schemes
builder.Services.AddSwaggerGen(options =>
{
    // Build a service provider temporarily to get the Document instance
    using var serviceProvider = builder.Services.BuildServiceProvider();
    var documentService = serviceProvider.GetRequiredService<IDocument>();
    var openApiDoc = documentService.GetOpenApiDocument();

    options.SwaggerDoc("v1", openApiDoc.Info);

    // Add BasicAuth and BearerAuth security definitions from your Document class
    foreach (var scheme in openApiDoc.Components.SecuritySchemes)
    {
        options.AddSecurityDefinition(scheme.Key, scheme.Value);
    }

    // Add security requirements (combined)
    foreach (var requirement in openApiDoc.SecurityRequirements)
    {
        options.AddSecurityRequirement(requirement);
    }

    // Avoid schema ID conflicts (nested classes)
    options.CustomSchemaIds(type => type.FullName.Replace("+", "."));
});

var app = builder.Build();

// Enable Swagger UI (enable in all environments or restrict with IsDevelopment if you want)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ITSC .NET Practice API v1");
});

// Apply pending EF Core migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    UserSeeder.Seed(db);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
