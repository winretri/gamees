using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.EventEmitter;
using Infrastructure.Events;
using Infrastructure.Riddles;
using Playing.Events;

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
        public void Handle(IEventSourcingEvent e)
        {
            switch (e.DomainEvent)
            {
                case Opened opened: Handle(opened);
                    break;
                case GuessMade guessMade:
                    Handle(guessMade);
                    break;
                case LevelSucceeded succeeded:
                    Handle(succeeded);
                    break;
                case LevelFailed failed:
                    Handle(failed);
                    break;
                case GameCompleted completed:
                    Handle(completed);
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
            grm.IsCompleted = false;
            this._context.Games.Add(grm);
            this._context.SaveChanges();
        }

        private void Handle(LevelSucceeded e)
        {
            GameReadModel grm = this._context.Games.FirstOrDefault(game => game.Id == e.Id);
            grm.Level = e.NewLevel;
            grm.Score = e.NewScore;
            grm.Question = _riddleRepository.GetRiddle(e.NewLevel).Question;

            GuessReadModel sourceGuess = this._context.Guesses.FirstOrDefault(g => g.Id == e.GuessId && g.GuessStatus == "unknown");
            sourceGuess.GuessStatus = "correct";

            this._context.SaveChanges();
        }

        private void Handle(GuessMade e)
        {
            GuessReadModel guessRM = new GuessReadModel() { GameId = e.GameId, GuessStatus = "unknown", Guess = e.Guess, Id = e.GuessId, Level = e.Level, GuessTime = DateTime.UtcNow};
            this._context.Guesses.Add(guessRM);
            this._context.SaveChanges();
        }

        private void Handle(GameCompleted e)
        {
            GameReadModel grm = this._context.Games.FirstOrDefault(game => game.Id == e.Id);
            grm.IsCompleted = true;
            this._context.SaveChanges();
        }

        private void Handle(LevelFailed e)
        {
            GameReadModel grm = this._context.Games.FirstOrDefault(game => game.Id == e.Id);
            grm.Score = e.NewScore;

            GuessReadModel sourceGuess = this._context.Guesses.FirstOrDefault(g => g.Id == e.GuessId && g.GuessStatus == "unknown");
            sourceGuess.GuessStatus = "wrong";

            this._context.SaveChanges();
        }

        public GameView Get(string id)
        {
            GameReadModel grm = _context.Games.FirstOrDefault( game => game.Id == id);
            if (grm != null)
            {
                GameView gameView = new GameView() {Id = grm.Id, Level = grm.Level, Question = grm.Question, Score = grm.Score, Completed = grm.IsCompleted };
                return gameView;
            }

            return null;
        }

        public IList<GuessView> GetGuesses(string id, int level)
        {
            List<GuessView> guessForGameAtLevel = _context.Guesses.Where(guess => guess.GameId == id && guess.Level == level).Select(grm => new GuessView() {Guess = grm.Guess,GuessStatus = grm.GuessStatus, Id = grm.Id, GuessTime = grm.GuessTime}).ToList();

            return guessForGameAtLevel;
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
