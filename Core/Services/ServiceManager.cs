using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services;

namespace Core.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IItemService> _lazyItemService;
        public ServiceManager(IRepositoryManager repositoryManager) 
        {
            _lazyItemService = new Lazy<IItemService>(() => new ItemService(repositoryManager.ItemRepository));
        }

        public IItemService ItemService => _lazyItemService.Value;
    }
}
