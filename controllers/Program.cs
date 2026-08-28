using controllers.Configuration;
using Core.Domain.Repositories.Abstactions;
using Core.Infrastructure;
using Core.Infrastructure.Repositories;
using Core.Services;
using Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

var conventionPack = new ConventionPack
{
    new CamelCaseElementNameConvention(),    // Auto-maps C# PascalCase -> BSON camelCase
    new IgnoreExtraElementsConvention(true)   // Prevents crashes if BSON has extra fields
};

ConventionRegistry.Register("GlobalConventions", conventionPack, _ => true);

// 1. Bind appsettings.json section to the configuration class
builder.Services.Configure<MongoDBSetting>(
    builder.Configuration.GetSection("MongoDB"));

// 2. Register AppDbContext with MongoDB EF Core Provider
builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    var mongoSettings = sp.GetRequiredService<IOptions<MongoDBSetting>>().Value;

    options.UseMongoDB(mongoSettings.ConnectionString, mongoSettings.DatabaseName)
           .UseCamelCaseNamingConvention(); // <--- Forces EF Core to map all properties to camelCase in MongoDB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();