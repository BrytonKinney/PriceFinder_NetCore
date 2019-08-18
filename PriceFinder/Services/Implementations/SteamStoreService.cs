using Microsoft.Extensions.Configuration;
using PriceFinder.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static PriceFinder.Constants.Models;

namespace PriceFinder.Services
{
    public class SteamStoreService : ISteamStore
    {
        private HttpClient _client;
        private readonly IModelProvider<SteamModel> _modelProvider;
        private readonly IConfiguration _configuration;

        public SteamStoreService(IModelProvider<SteamModel> modelProvider, IConfiguration configuration)
        {
            _modelProvider = modelProvider;
            _configuration = configuration;
            _client = new HttpClient();
        }

        public async Task<IEnumerable<ProductModel>> Search(string searchTerm)
        {
            var searchUrl = $"{_configuration.GetSection("BaseUrls")["SteamStore"]}/search/suggest?term={searchTerm}&f=games&cc=US&l=english";
            var response = await _client.GetAsync(searchUrl);
            var html = await response.Content.ReadAsStringAsync();
            return await _modelProvider.BuildAsync(html);
        }
    }
}
