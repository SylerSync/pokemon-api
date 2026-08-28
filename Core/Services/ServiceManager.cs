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
        private readonly Lazy<IPokemonService> _lazyPokemonService;
        private readonly Lazy<IUserService> _lazyUserService;

        public ServiceManager(IRepositoryManager repositoryManager) 
        {
            _lazyItemService = new Lazy<IItemService>(() => new ItemService(repositoryManager.ItemRepository));
            _lazyPokemonService = new Lazy<IPokemonService>(() => new PokemonService(repositoryManager.PokemonRepository));
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager.UserRepository));
        }

        public IItemService ItemService => _lazyItemService.Value;
        public IPokemonService IPokemonService => _lazyPokemonService.Value;
        public IUserService IUserService => _lazyUserService.Value;
    }
}
