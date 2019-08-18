using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using PriceFinder.DTO;
using PriceFinder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PriceFinder.Constants.Models;

namespace PriceFinder.Services
{
    public class SteamStoreModelProvider : IModelProvider<SteamModel>
    {
        private readonly IConfiguration _configuration;

        public SteamStoreModelProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<ProductModel>> BuildAsync(string html)
        {
            var modelMatch = _configuration.GetSection("ModelFormat:SteamStore").Get<ResultDto>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var itemNodes = htmlDoc.DocumentNode.SelectNodes($"//a");
            return BuildProductModelArray(itemNodes, modelMatch);
        }

        public IEnumerable<ProductModel> BuildProductModelArray(HtmlNodeCollection nodes, ResultDto match)
        {
            foreach (var node in nodes)
            {
                var childNodes = node.ChildNodes;
                var itemName = childNodes[0].InnerText;
                var price = childNodes[2].InnerText;
                yield return new ProductModel { ItemName = itemName, Price = price };
            }
        }
    }
}
