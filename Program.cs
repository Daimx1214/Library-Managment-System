using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ── Database ──────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// ── Swagger ───────────────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title    = "Library Management System API",
        Version  = "v1",
        Description = "Full CRUD REST API for Library Management System."
    });
    c.CustomSchemaIds(type => type.FullName!.Replace("+", "_"));
});

// ── CORS ──────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// ── Migrate DB (non-blocking — app runs even if DB is down) ───────────────────
_ = Task.Run(async () =>
{
    await Task.Delay(2000); // wait for app to fully start first
    try
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
        Console.WriteLine("✅ Database migration completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️  DB migration failed: {ex.Message}");
        Console.WriteLine("App is still running. Fix connection string and restart.");
    }
});

// ── Middleware ────────────────────────────────────────────────────────────────
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS API v1");
    c.RoutePrefix    = "swagger";
    c.DocumentTitle  = "LMS API Docs";
    c.DefaultModelsExpandDepth(-1);
});

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Redirect root → swagger
app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
   .ExcludeFromDescription();

app.Run();
