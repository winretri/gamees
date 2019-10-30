using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NeverCompletedGame.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly IViewStore _viewStore;

        public ViewController(IViewStore viewStore)
        {
            _viewStore = viewStore;
        }

        [HttpGet("games/{id}")]
        public ActionResult<GameView> Get(string id)
        {
            GameView game = _viewStore.Get(id);
            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpGet("games/{id}/guesses/{level}")]
        public ActionResult<IList<GuessView>> GetGuesses(string id, int level)
        {
            IList<GuessView> guesses = _viewStore.GetGuesses(id, level);
            if (guesses == null)
            {
                return NotFound();
            }

            return Ok(guesses);
        }

    }
}
