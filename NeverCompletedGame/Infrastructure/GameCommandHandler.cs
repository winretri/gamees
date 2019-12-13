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
        private readonly IRepository repo;
        private readonly IEventBus eventBus;

        public GameCommandHandler(IRepository repo, IEventBus eventBus)
        {
            this.repo = repo;
            this.eventBus = eventBus;
        }

        public void Handle(string aggregateId, ICommand command)
        {
            Aggregate newGame = repo.Restore(aggregateId) ?? new Aggregate(new Game());
            command.Handle(newGame.Game);
            repo.Save(newGame);
            eventBus.Emit(newGame.UncomittedEvents);
        }
    }
}
