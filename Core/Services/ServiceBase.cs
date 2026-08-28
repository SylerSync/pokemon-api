using Core.Domain.Repositories.Abstactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    internal abstract class ServiceBase
    {
        protected readonly IRepositoryManager _repositoryManager;

        public ServiceBase(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
    }
}
