using Core.Domain.Repositories.Abstactions;
using Core.Infrastructure.MongoSeedData;
using Core.Infrastructure.Repositories;
using Core.Services;
using Core.Services.Abstractions;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("MongoDb") ?? "mongodb://localhost:27018";
string databaseName = builder.Configuration["DatabaseSettings:DatabaseName"] ?? "PokemonDb";

builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
            var seeder = new MongoDataSeeder(database);

            // Execute seeder synchronously for startup
            Task.Run(async () => await seeder.SeedAllAsync()).Wait();

            Console.WriteLine("[Startup] Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Startup] Error during database seeding: {ex.Message}");
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
