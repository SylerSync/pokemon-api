using Core.Domain.Repositories.Abstactions;
using MongoDB.Driver;
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
        
        public RepositoryManager(MongoContext database) 
        {
            _lazyItemRepository = new Lazy<IItemRepository>(() => new ItemRepository());
            _lazyPokemonRepo = new Lazy<IPokemonRepository>(() => new PokemonRepository(database));
        }

        public IItemRepository ItemRepository => _lazyItemRepository.Value;
        public IPokemonRepository PokemonRepository => _lazyPokemonRepo.Value;
    }
}
