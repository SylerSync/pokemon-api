using Core.Domain.DataObjects;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        public MongoContext(IMongoDatabase database) => _database = database;

        public IMongoCollection<Pokemon> Pokemon => _database.GetCollection<Pokemon>("pokemon");
    }
}
