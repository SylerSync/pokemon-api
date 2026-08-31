using Core.Domain.DataObjects;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Core.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<BaseItem> Items { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Map User Collection
        modelBuilder.Entity<User>(b =>
        {
            b.ToCollection("users");

            // Explicitly set Email as the Key and map it to MongoDB's '_id' field
            b.HasKey(u => u.Email);
            b.Property(u => u.Email).HasElementName("_id");
        });

        // Map BaseItem Collection, _id, and Polymorphic Discriminators
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

        // Map Concrete Owned ItemEffect (EF Core handles camelCase for properties automatically)
        modelBuilder.Entity<RecoveryItem>(b =>
        {
            b.OwnsOne(r => r.Effect);
        });

        modelBuilder.Entity<Pokemon>(p =>
        {
            p.ToCollection("pokemon");
            p.Property(i => i._id).HasElementName("_id");
        });
    }
}