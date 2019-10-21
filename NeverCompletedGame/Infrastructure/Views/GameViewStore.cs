using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Riddles;
using Playing;

namespace Infrastructure.Views
{
    public class GameViewStore : IViewStore
    {
        private readonly GameViewDbContext _context;
        private readonly IRiddleRepository _riddleRepository;

        public GameViewStore(GameViewDbContext context, IRiddleRepository riddleRepository)
        {
            _context = context;
            _riddleRepository = riddleRepository;
        }
        public void Handle(IEvent e)
        {
            switch (e)
            {
                case Opened opened: Handle(opened);
                break;
            }
        }

        private void Handle(Opened e)
        {
            GameReadModel grm = new GameReadModel();
            grm.Id = e.Id;
            grm.Level = e.Level;
            grm.Question = "";
            grm.Score = e.Score;
        }

        public GameView Get(string id)
        {
            GameReadModel grm = _context.Games.FirstOrDefault( game => game.Id == id);
            if (grm != null)
            {
                GameView gameView = new GameView() {Id = grm.Id, Level = grm.Level, Question = grm.Question, Score = grm.Score};
                return gameView;
            }

            return null;
        }
    }
}
