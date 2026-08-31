using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IItemService> _lazyItemService;
        private readonly Lazy<IPokemonService> _lazyPokemonService;
        private readonly Lazy<IUserService> _lazyUserService;
        private readonly Lazy<IPasswordService> _lazyPasswordService;

        public ServiceManager(IRepositoryManager repositoryManager, IPasswordHasher<User> passwordHasher) 
        {
            _lazyItemService = new Lazy<IItemService>(() => new ItemService(repositoryManager));
            _lazyPokemonService = new Lazy<IPokemonService>(() => new PokemonService(repositoryManager));
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, passwordHasher));
            _lazyPasswordService = new Lazy<IPasswordService>(() => new PasswordService(passwordHasher));
        }

        public IItemService ItemService => _lazyItemService.Value;
        public IPokemonService PokemonService => _lazyPokemonService.Value;
        public IUserService UserService => _lazyUserService.Value;
        public IPasswordService PasswordService => _lazyPasswordService.Value;
    }
}
