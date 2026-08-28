using controllers.Configuration;
using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Core.Infrastructure;
using Core.Infrastructure.Repositories;
using Core.Services;
using Core.Services.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();



// Global Conventions
var conventionPack = new ConventionPack
{
    new CamelCaseElementNameConvention(),
    new IgnoreExtraElementsConvention(true)
};
ConventionRegistry.Register("CamelCase", conventionPack, type => true);

// Register BaseItem base class and Abstract classes
BsonClassMap.RegisterClassMap<BaseItem>(cm =>
{
    cm.AutoMap();
    cm.SetIsRootClass(true);
});

BsonClassMap.RegisterClassMap<EvolutionItem>();
BsonClassMap.RegisterClassMap<MegaEvolutionItem>();
BsonClassMap.RegisterClassMap<PokeballItem>();
BsonClassMap.RegisterClassMap<RecoveryItem>();
BsonClassMap.RegisterClassMap<TechnicalMachineItem>();

// Register ItemEffect base class and Abstract classes
BsonClassMap.RegisterClassMap<ItemEffect>(cm =>
{
    cm.AutoMap();
    cm.SetIsRootClass(true);
});
BsonClassMap.RegisterClassMap<HealEffect>();
BsonClassMap.RegisterClassMap<StatusHealEffect>();
BsonClassMap.RegisterClassMap<PpHealEffect>();
BsonClassMap.RegisterClassMap<PpMaxRaise>();
BsonClassMap.RegisterClassMap<ReviveEffect>();

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
