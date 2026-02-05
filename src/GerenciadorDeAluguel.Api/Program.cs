using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using GerenciadorDeAluguel.Infrastructure;
using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Infrastructure.EfRepositories;
using GerenciadorDeAluguel.Application.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });


// ============================================
//  ENTITY FRAMEWORK CORE (SQLite In-Memory)
// ============================================
// Create a shared connection that will be reused across all DbContext instances


var sharedConnection = new SqliteConnection("DataSource=:memory:");
sharedConnection.Open();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(sharedConnection); // Use the SAME connection for all requests
    
    // Development helpers (shows SQL queries and parameter values in logs)
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});


// ============================================
//  DI (Infrastructure → Application Ports)
// ============================================
builder.Services.AddScoped<IClientRepository, EfClientRepository>();
builder.Services.AddScoped<IPropertyRepository, EfPropertyRepository>();
builder.Services.AddScoped<IReservationRepository, EfReservationRepository>();


// ============================================
//  DI (Application Services)
// ============================================
builder.Services.AddScoped<RegisterClientService>();
builder.Services.AddScoped<RegisterPropertyService>();
builder.Services.AddScoped<CreateReservationService>();
builder.Services.AddScoped<CancelReservationService>();
builder.Services.AddScoped<ChangePropertyStatusService>();
builder.Services.AddScoped<CheckPropertyAvailabilityService>();
builder.Services.AddScoped<GetPropertyByIdService>();
builder.Services.AddScoped<GetReservationByIdService>();
builder.Services.AddScoped<ListAvailablePropertiesService>();
builder.Services.AddScoped<ListReservationsService>();


// ============================================
//  SWAGGER
// ============================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gerenciador de Aluguel API",
        Version = "v1",
        Description = "API para gerenciamento de clientes, imóveis e reservas",
        Contact = new OpenApiContact
        {
            Name = "DTI Test Project"
        }
    });
    
    // Enable XML documentation comments in Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});


var app = builder.Build();


// ============================================
//  Initialize In-Memory DB
// ============================================
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
    Console.WriteLine("✅ Database initialized successfully");
}

// Register cleanup on shutdown
app.Lifetime.ApplicationStopping.Register(() =>
{
    sharedConnection.Close();
    sharedConnection.Dispose();
    Console.WriteLine("🔒 Database connection closed");
});


// ============================================
//  Middleware Pipeline
// ============================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerenciador de Aluguel API v1");
        options.RoutePrefix = string.Empty; // Swagger at root: https://localhost:5001/
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Make Program accessible to integration tests
public partial class Program { }
