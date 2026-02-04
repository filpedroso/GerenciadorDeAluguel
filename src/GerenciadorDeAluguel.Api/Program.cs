using Microsoft.EntityFrameworkCore;
using GerenciadorDeAluguel.Infrastructure;
using GerenciadorDeAluguel.Application.Ports;
using GerenciadorDeAluguel.Infrastructure.EfRepositories;
using GerenciadorDeAluguel.Application.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// 1. CONTROLLERS
// ============================================
builder.Services.AddControllers();

// ============================================
// 2. ENTITY FRAMEWORK CORE (SQLite In-Memory)
// ============================================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("DataSource=:memory:");
    
    // Development helpers (shows SQL queries and parameter values in logs)
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// ============================================
// 3. REPOSITORIES (Infrastructure → Application Ports)
// ============================================
builder.Services.AddScoped<IClientRepository, EfClientRepository>();
builder.Services.AddScoped<IPropertyRepository, EfPropertyRepository>();
builder.Services.AddScoped<IReservationRepository, EfReservationRepository>();

// ============================================
// 4. APPLICATION SERVICES
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
// 5. SWAGGER/OPENAPI
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
    
    // Optional: Enable XML documentation comments in Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// ============================================
// BUILD APPLICATION
// ============================================
var app = builder.Build();

// ============================================
// 6. INITIALIZE IN-MEMORY DATABASE
// ============================================
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // CRITICAL: Keep connection open for SQLite in-memory
    // Without this, the database disappears after this block
    dbContext.Database.OpenConnection();
    
    // Create tables based on DbSet entities
    dbContext.Database.EnsureCreated();
    
    // Optional: Seed initial test data
    // SeedDatabase(dbContext);
}

// ============================================
// 7. MIDDLEWARE PIPELINE
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


