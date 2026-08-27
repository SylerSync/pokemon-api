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
        private readonly Lazy<IPokemonRepository> _lazyPokemonRepo;
        public IPokemonRepository PokemonRepository => _lazyPokemonRepo.Value;
    }
}
