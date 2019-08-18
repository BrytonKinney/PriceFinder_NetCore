using PriceFinder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PriceFinder.Services
{
    public interface ISteamStore
    {
        Task<IEnumerable<ProductModel>> Search(string searchTerm);
    }
}
