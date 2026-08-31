using Core.Domain.DataObjects;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Core.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<BaseItem> Items { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Map BaseItem Collection, _id, and Polymorphic Discriminators
        modelBuilder.Entity<BaseItem>(b =>
        {
            b.ToCollection("items")
             .HasDiscriminator<string>("_t")
             .HasValue<EvolutionItem>("EvolutionItem")
             .HasValue<MegaEvolutionItem>("MegaEvolutionItem")
             .HasValue<PokeballItem>("PokeballItem")
             .HasValue<RecoveryItem>("RecoveryItem")
             .HasValue<TechnicalMachineItem>("TechnicalMachineItem");

            // Explicitly bind 'Id' to MongoDB's standard '_id' field
            b.Property(i => i.Id).HasElementName("_id");
        });

        // 2. Map Concrete Owned ItemEffect (EF Core handles camelCase for properties automatically)
        modelBuilder.Entity<RecoveryItem>(b =>
        {
            b.OwnsOne(r => r.Effect);
        });

        modelBuilder.Ignore<Pokemon>();
    }
}