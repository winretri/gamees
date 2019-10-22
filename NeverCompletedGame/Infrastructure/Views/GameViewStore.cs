using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.EventEmitter;
using Infrastructure.Riddles;
using Playing;

namespace Infrastructure.Views
{
    public sealed class GameViewStore : IViewStore, IDisposable
    {
        private readonly GameViewDbContext _context;
        private readonly IRiddleRepository _riddleRepository;
        private readonly IDisposable _subscription;

        public GameViewStore(GameViewDbContext context, IRiddleRepository riddleRepository, IEventBus eventBus)
        {
            _context = context;
            _riddleRepository = riddleRepository;
            _subscription = eventBus.Events.Subscribe(args => Handle(args.EventArgs));
        }
        public void Handle(IEvent e)
        {
            switch (e)
            {
                case Opened opened: Handle(opened);
                    break;
                case LevelSucceeded succeeded:
                    Handle(succeeded);
                    break;
                case LevelFailed failed:
                    Handle(failed);
                    break;
            }
        }

        private void Handle(Opened e)
        {
            GameReadModel grm = new GameReadModel();
            grm.Id = e.Id;
            grm.Level = e.Level;
            grm.Question = _riddleRepository.GetRiddle(e.Level).Question;
            grm.Score = e.Score;
            this._context.Games.Add(grm);
            this._context.SaveChanges();
        }

        private void Handle(LevelSucceeded e)
        {
            GameReadModel grm = this._context.Games.FirstOrDefault(game => game.Id == e.Id);
            grm.Level = e.NewLevel;
            grm.Score = e.NewScore;
            grm.Question = _riddleRepository.GetRiddle(e.NewLevel).Question;
            this._context.SaveChanges();
        }

        private void Handle(LevelFailed e)
        {
            GameReadModel grm = this._context.Games.FirstOrDefault(game => game.Id == e.Id);
            grm.Score = e.NewScore;
            this._context.SaveChanges();
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

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
