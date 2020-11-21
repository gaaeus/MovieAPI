using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        // GET: <MetadataController>
        //[HttpGet]
        //public async Task<IEnumerable<Metadata>> Get()
        //{
        //    var common = new Common();

        //    var metadata = await common.GetMetadata();
        //    return metadata;
        //}

        // GET <MetadataController>/5
        [HttpGet("{movieId}")]
        public async Task<IEnumerable<Metadata>> Get(int movieId)
        {
            var common = new Common();

            var metadata = await common.GetMetadata();

            var results = metadata.Where(x => x.MovieId.Equals(movieId));

            return results;
        }

        // POST <MetadataController>
        [HttpPost]
        public ActionResult<string> Post([FromBody]Metadata metadata)
        {
            if (ModelState.IsValid)
            {
                var metadataList = new List<Metadata>();

                metadataList.Add(metadata);

                return Content("Metadata added to database");
            }
            else
            {
                return Content("Metadata invalid and not added to database");
            }
        }
    }
}
