using System;
using Microsoft.AspNet.Mvc;

namespace AspNetCoreService.Controllers
{
    [Route("api/health")]
    public class HealthController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}