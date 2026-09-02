using Core.Domain.Repositories.Abstactions;
using Microsoft.EntityFrameworkCore;
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
        private readonly Lazy<IInventoryrepository> _lazyInventoryRepository;
        private readonly Lazy<IPokeBoxRepository> _lazyPokeBoxRepository;

        public RepositoryManager(AppDbContext database) 
        {
            _lazyItemRepository = new Lazy<IItemRepository>(() => new ItemRepository(database));
            _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(database));
            _lazyPokemonRepo = new Lazy<IPokemonRepository>(() => new PokemonRepository(database));
            _lazyInventoryRepository = new Lazy<IInventoryrepository>(() => new InventoryRepository(database));
            _lazyPokeBoxRepository = new Lazy<IPokeBoxRepository>(() => new PokeBoxRepository(database));
        }

        public IItemRepository ItemRepository => _lazyItemRepository.Value;
        public IPokemonRepository PokemonRepository => _lazyPokemonRepo.Value;
        public IUserRepository UserRepository => _lazyUserRepository.Value;
        public IPokeBoxRepository PokeBoxRepository => _lazyPokeBoxRepository.Value;
        public IInventoryrepository InventoryRepository => _lazyInventoryRepository.Value;
    }
}
