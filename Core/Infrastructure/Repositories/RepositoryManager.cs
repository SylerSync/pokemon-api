using Core.Domain.Repositories.Abstactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IItemRepository> _lazyItemRepository;
        private readonly Lazy<IPokemonRepository> _lazyPokemonRepo;
        private readonly Lazy<IUserRepository> _lazyUserRepository;
        
        public RepositoryManager() 
        {
            _lazyItemRepository = new Lazy<IItemRepository>(() => new ItemRepository());
            _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository());
        }

        public IItemRepository ItemRepository => _lazyItemRepository.Value;
        public IPokemonRepository PokemonRepository => _lazyPokemonRepo.Value;
        public IUserRepository UserRepository => _lazyUserRepository.Value;
    }
}
