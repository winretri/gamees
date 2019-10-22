using System;
using System.Windows.Input;
using Playing;
using ICommand = Playing.ICommand;
using Infrastructure.EventEmitter;

namespace Infrastructure
{
    public class GameCommandHandler : ICommandHandler<ICommand>, ICommandHandler<Open>, ICommandHandler<Close>
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
            Game newGame = repo.Restore(aggregateId) ?? new Game();
            command.Handle(newGame);
            repo.Save(newGame);
            eventBus.Emit(newGame.UncomittedEvents);
        }

        public void Handle(string aggregateId, MakeGuess command)
        {
            Game newGame = repo.Restore(aggregateId) ?? new Game();
            command.Handle(newGame);
            repo.Save(newGame);
            eventBus.Emit(newGame.UncomittedEvents);
        }

        public void Handle(string aggregateId, Close command)
        {
            Game newGame = repo.Restore(aggregateId) ?? new Game();
            command.Handle(newGame);
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
