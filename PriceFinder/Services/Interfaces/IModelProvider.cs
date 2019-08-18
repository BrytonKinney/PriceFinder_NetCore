using PriceFinder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PriceFinder.Constants.Models;

namespace PriceFinder.Services
{
    public interface IModelProvider<T> where T : ModelType
    {
        Task<IEnumerable<ProductModel>> BuildAsync(string html);
    }
}
