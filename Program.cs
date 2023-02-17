using Commander.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ======================================
// Define our DI dependencies
// ======================================

// Our domain configuration
builder.Services.AddDbContext<CommanderContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("CommanderConnection")));

// The Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore NuGet package provides ASP.NET Core middleware for Entity Framework Core error pages. 
// This middleware helps to detect and diagnose errors with Entity Framework Core migrations.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Repositories
builder.Services.AddScoped<ICommanderRepository, CommanderRepository>();

// Service

// Mappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ======================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    // Entity Framework Core error pages
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    app.UseExceptionHandler("/Error");
    // app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CommanderContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);

    // Migrations
    // Restore migrations, type in the terminal:
    // dotnet ef database update
    // To create migration:
    // dotnet ef migrations add InitialMigration
    // TODO: Read more https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
