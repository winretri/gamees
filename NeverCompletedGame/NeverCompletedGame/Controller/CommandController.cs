
using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeverCompletedGame.Commands;
using NeverCompletedGame.Events;
using NeverCompletedGame.Infrastructure;
using Newtonsoft.Json;
using Playing;
using Playing.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NeverCompletedGame.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly ICommandFactory _commandFactory;
        private readonly IViewStore _viewStore;
        private readonly IEventBusHubBridge _bridge;

        public CommandController(ICommandHandlerFactory commandHandlerFactory, ICommandFactory commandFactory, IViewStore viewStore, IEventBusHubBridge bridge)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _commandFactory = commandFactory;
            _viewStore = viewStore;
            _bridge = bridge;
        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Command c)
        {
            var ch = this._commandHandlerFactory.GetCommandHandler<ICommand>(c.Domain, c.AggregateType, c.Name);
            var com = this._commandFactory.GetCommand<ICommand>(c.Domain, c.AggregateType, c.Name, c.Payload);
            ch.Handle(c.AggregateId.ToString(), com);
            Ok();
        }

    }
}
