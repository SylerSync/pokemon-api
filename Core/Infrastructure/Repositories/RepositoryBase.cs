using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Repositories
{
    internal abstract class RepositoryBase
    {
        protected readonly AppDbContext _dbContext;

        protected RepositoryBase(AppDbContext dbContext) => _dbContext = dbContext;
    }
}
