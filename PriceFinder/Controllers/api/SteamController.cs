using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PriceFinder.Models;
using PriceFinder.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PriceFinder.Controllers.api
{
    [Route("[controller]")]
    [ApiController]
    public class SteamController : ControllerBase
    {

        private readonly ISteamStore _steamService;

        public SteamController(ISteamStore steamSvc)
        {
            _steamService = steamSvc;
        }

        [HttpGet]
        [Route("{searchTerm}")]
        public async Task<IEnumerable<ProductModel>> Search(string searchTerm)
        {
            return await _steamService.Search(searchTerm);
        }
    }
}