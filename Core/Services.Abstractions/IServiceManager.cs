using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Abstractions
{
    public interface IServiceManager
    {
        IItemService ItemService { get; }
        IPokemonService PokemonService { get; }
        IUserService UserService { get; }
        IPasswordService PasswordService { get; }
        IInventoryService InventoryService { get; }
        IPokeBoxService PokeBoxService { get; }
    }
}
