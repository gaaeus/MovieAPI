using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieAPI.Controllers
{
    [Route("movies/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        // GET: movies/<StatsController>
        [HttpGet]
        public async Task<IEnumerable<Stats>> Get()
        {
            var common = new Common();

            var stats = await common.GetStats();
            return stats;
        }
    }
}
