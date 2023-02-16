using Commander.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ======================================
// Define our DI dependencies
// ======================================
builder.Services.AddSingleton<ICommanderRepository, MockCommanderRepository>();

// Our DB configuration
builder.Services.AddDbContext<CommanderContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("CommanderConnection")));

// The Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore NuGet package provides ASP.NET Core middleware for Entity Framework Core error pages. This middleware helps to detect and diagnose errors with Entity Framework Core migrations.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// ======================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Entity Framework Core error pages
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CommanderContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
