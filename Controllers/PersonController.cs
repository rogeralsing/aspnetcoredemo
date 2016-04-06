using System.Linq;
using System.Threading.Tasks;
using AspNetCoreService.Services;
using Microsoft.AspNet.Mvc;


namespace AspNetCoreService.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        [HttpGet("{name}")]
        public async Task<dynamic> Get(string name, [FromServices]CouchDb couchDb)
        {
            return await couchDb.GetDocument(name);
        }

        [HttpPut("{name}")]
        public async Task Put(string name,[FromBody] dynamic person, [FromServices]CouchDb couchDb)
        {
            await couchDb.PutDocument(name, person);
        }
    }
}