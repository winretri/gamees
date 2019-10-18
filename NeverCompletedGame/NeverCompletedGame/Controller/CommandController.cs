
using System;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeverCompletedGame.Commands;
using Newtonsoft.Json;
using Playing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NeverCompletedGame.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandController(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            const string sessionKey = "FirstSeen";
            DateTime dateFirstSeen;
            var value = HttpContext.Session.GetString(sessionKey);
            if (string.IsNullOrEmpty(value))
            {
                dateFirstSeen = DateTime.Now;
                var serialisedDate = JsonConvert.SerializeObject(dateFirstSeen);
                HttpContext.Session.SetString(sessionKey, serialisedDate);
            }
            else
            {
                dateFirstSeen = JsonConvert.DeserializeObject<DateTime>(value);
            }
            return new string[] { "value1", "value2", dateFirstSeen.ToString() };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Command c)
        {
            var ch = this._commandHandlerFactory.GetCommandHandler<ICommand>(c.Domain, c.AggregateType, c.Name);
        }

    }
}
