using System;
using System.Windows.Input;
using Playing;
using ICommand = Playing.Commands.ICommand;
using Infrastructure.EventEmitter;
using Playing.Commands;

namespace Infrastructure
{
    public class GameCommandHandler : ICommandHandler<ICommand>, ICommandHandler<Open>, ICommandHandler<Close>, ICommandHandler<MakeGuess>
    {
        private IRepository repo;
        private IEventBus eventBus;

        public GameCommandHandler(IRepository repo, IEventBus eventBus)
        {
            this.repo = repo;
            this.eventBus = eventBus;
        }

        public void Handle(string aggregateId, Open command)
        {
            Aggregate newGame = repo.Restore(aggregateId) ?? new Aggregate(new Game());
            command.Handle(newGame.Game);
            repo.Save(newGame);
            eventBus.Emit(newGame.UncomittedEvents);
        }

        public void Handle(string aggregateId, MakeGuess command)
        {
            Aggregate newGame = repo.Restore(aggregateId) ?? new Aggregate(new Game());
            command.Handle(newGame.Game);
            repo.Save(newGame);
            eventBus.Emit(newGame.UncomittedEvents);
        }

        public void Handle(string aggregateId, Close command)
        {
            Aggregate newGame = repo.Restore(aggregateId) ?? new Aggregate(new Game());
            command.Handle(newGame.Game);
            repo.Save(newGame);
            eventBus.Emit(newGame.UncomittedEvents);
        }

        public void Handle(string aggregateId, ICommand command)
        {
            switch (command)
            {
                case Open o:
                    this.Handle(aggregateId, o);
                    break;
                case MakeGuess mg:
                    this.Handle(aggregateId, mg);
                    break;
                case Close c:
                    this.Handle(aggregateId, c);
                    break;

            }
        }
    }
}
