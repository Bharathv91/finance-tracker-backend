using FinanceTracker.Application.Interfaces;
using FinanceTracker.Application.Services;
using FinanceTracker.Infrastructure.Data;
using FinanceTracker.Infrastructure.Persistence;
using FinanceTracker.Infrastructure.Repositories;
using FinanceTracker.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure ports explicitly
builder.WebHost.UseUrls("https://localhost:5001", "http://localhost:5000");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext
var conn = builder.Configuration.GetConnectionString("DefaultConnection")
           ?? "Server=localhost,1433;Database=FinanceTrackerDb;User Id=sa;Password=Your_password123;";
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(conn));

// Register repositories, uow, services
builder.Services.AddScoped<IExpenseRepository, EfExpenseRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ExpenseService>();

var app = builder.Build();

// Seed DB and apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<AppDbContext>();
        // apply migrations (creates DB if not exist)
        db.Database.Migrate();
        Console.WriteLine("? Migrations applied successfully");
    
        // seed sample data
        await DbSeeder.SeedAsync(db);
        Console.WriteLine("? Database seeded successfully");
    }
    catch (Exception ex)
    {
        // In real apps, use proper logging. Keep console fallback here.
        Console.WriteLine($"?? DB migration/seeding warning: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        // Don't throw - let the app continue running so we can debug
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Disable HTTPS redirect in development to avoid SSL issues
}
else
{
 app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("?? Starting FinanceTracker API on https://localhost:5001 and http://localhost:5000");
app.Run();
