using controllers.Configuration;
using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Core.Infrastructure;
using Core.Infrastructure.Repositories;
using Core.Services;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.Configure<MongoSeedOptions>(builder.Configuration.GetSection("MongoSeed"));

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
var mongoSettings = builder.Configuration.GetSection("MongoDB").Get<MongoDBSetting>()
    ?? throw new InvalidOperationException("Missing 'MongoDB' configuration section.");

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoSettings.ConnectionString));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoSettings.DatabaseName));

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    var mongoSettings = sp.GetRequiredService<IOptions<MongoDBSetting>>().Value;

    options.UseMongoDB(mongoSettings.ConnectionString, mongoSettings.DatabaseName)
           .UseCamelCaseNamingConvention(); // <--- Forces EF Core to map all properties to camelCase in MongoDB
});

builder.Services.AddSingleton<MongoSeeder>();

builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

await app.Services.GetRequiredService<MongoSeeder>().SeedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();