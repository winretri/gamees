using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NeverCompletedGame.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        // GET: api/View
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/View/5
        [HttpGet("game/{id}")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
