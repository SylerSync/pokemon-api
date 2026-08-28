using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Repositories
{
    internal abstract class RepositoryBase
    {
        protected readonly MongoContext _dbContext;

        protected RepositoryBase(MongoContext dbContext) => _dbContext = dbContext;
    }
}
