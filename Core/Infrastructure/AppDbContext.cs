using Core.Domain.DataObjects;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;
using MongoDB.EntityFrameworkCore.ValueGeneration;

namespace Core.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<BaseItem> Items { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<CaughtPokemon> CaughtPokemon { get; set; }

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

        // Map Pokemon Collection
        modelBuilder.Entity<Pokemon>(p =>
        {
            p.ToCollection("pokemon");
            p.Property(i => i._id).HasElementName("_id");
        });

        // Map CaughtPokemon Collection
        modelBuilder.Entity<CaughtPokemon>(p =>
        {
            p.ToCollection("caught_pokemon");
            p.HasKey(x => x._id);
            p.Property(i => i._id).HasElementName("_id")
                .HasElementName("_id")
                .HasConversion<ObjectId>()                          // store as a native ObjectId
                .HasValueGenerator<StringObjectIdValueGenerator>(); // generate one on insert
        });

        ////Map PokeBox collection
        //modelBuilder.Entity<PokeBox>(box =>
        //{
        //    box.ToCollection("pokebox");
        //    box.HasKey(b => b.UserID);
        //    box.Property(b => b.UserID).HasElementName("_id");

        //    box.OwnsMany(b => b.pokemon, poke =>
        //    {
        //        poke.OwnsOne(p => p.Sprites);
        //        poke.OwnsMany(p => p.Stats);
        //        poke.OwnsMany(p => p.Moves);
        //        poke.OwnsMany(p => p.EvolutionReqs);
        //    });
        //});

        // Map Inventory collection
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToCollection("inventory");
            entity.HasKey(i => i.UserEmail);
            entity.Property(i => i.UserEmail).HasElementName("_id");
            entity.OwnsMany(i => i.Items);
        });
    }
}