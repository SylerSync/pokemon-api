using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.DataObjects;
using MongoDB.Driver;

namespace Core.Infrastructure.MongoSeedData
{
    public class MongoDataSeeder
    {
        private readonly IMongoDatabase _database;

        public MongoDataSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task SeedAllAsync()
        {
            var seedDir = Path.Combine(AppContext.BaseDirectory, "MongoSeedData");
            //Call seeding for Items
            await SeedCollectionFromNdjsonAsync<BaseItem>("Items", Path.Combine(seedDir, "items.ndjson"));
        }

        private async Task SeedCollectionFromNdjsonAsync<T>(string collectionName, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[Seeder] Warning: File not found at {filePath}!");
                return;
            }

            var collection = _database.GetCollection<T>(collectionName);

            await _database.DropCollectionAsync(collectionName);

            var documents = new List<T>();
            string line;
            using (var reader = new StreamReader(filePath))
            {
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var document = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(line);

                    if (document != null)
                    {
                        documents.Add(document);
                    }
                }
            }
            if (documents.Count > 0)
            {
                await collection.InsertManyAsync(documents);
                Console.WriteLine($"[Seeder] Successfully seeded {documents.Count} documents into '{collectionName}' collection.");
            }
        }
    }
}
