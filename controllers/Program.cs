using controllers.Configuration;
using Core.Domain.Repositories.Abstactions;
using Core.Infrastructure;
using Core.Infrastructure.Repositories;
using Core.Services;
using Core.Services.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

// 1. Bind appsettings.json section to the configuration class
builder.Services.Configure<MongoDBSetting>(
    builder.Configuration.GetSection("MongoDB"));
// 2. Register MongoClient as a singleton
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(sp.GetRequiredService<IOptions<MongoDBSetting>>().Value.ConnectionString));

// 3. Register the specific database instance
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>()
      .GetDatabase(sp.GetRequiredService<IOptions<MongoDBSetting>>().Value.DatabaseName));

builder.Services.AddSingleton<MongoContext>();


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
