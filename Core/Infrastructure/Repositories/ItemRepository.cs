using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace Core.Infrastructure.Repositories
{
    internal sealed class ItemRepository : RepositoryBase, IItemRepository
    {
        public ItemRepository(MongoContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<BaseItem>> GetAllAsync()
        {
            // Fetch raw documents to validate discriminators before deserializing
            var rawCollection = _dbContext.Item.Database.GetCollection<MongoDB.Bson.BsonDocument>("items");
            var docs = await rawCollection.Find(MongoDB.Bson.BsonDocument.Parse("{}")).ToListAsync();

            var validTypes = new HashSet<string>
    {
        "EvolutionItem", "MegaEvolutionItem", "PokeballItem", "RecoveryItem", "TechnicalMachineItem"
    };

            foreach (var doc in docs)
            {
                string id = doc.Contains("_id") ? doc["_id"].ToString() : "UNKNOWN";

                // Check 1: Missing _t field
                if (!doc.Contains("_t"))
                {
                    throw new InvalidOperationException($"Document _id '{id}' is MISSING the '_t' field in MongoDB.");
                }

                // Check 2: Invalid or misspelled _t value
                string discriminator = doc["_t"].AsString;
                if (!validTypes.Contains(discriminator))
                {
                    throw new InvalidOperationException($"Document _id '{id}' has unmatched _t value: '{discriminator}'.");
                }
            }

            // Original Query
            return await _dbContext.Item.Find(Builders<BaseItem>.Filter.Empty).ToListAsync();
        }

        public Task<IReadOnlyList<BaseItem>> GetByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task<BaseItem> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
