using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Repositories.Abstactions
{
    public interface IRepositoryManager
    {
        IItemRepository ItemRepository { get; }
        IPokemonRepository PokemonRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
