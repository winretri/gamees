using System;
using System.Windows.Input;
using Playing;
using ICommand = Playing.ICommand;

namespace Infrastructure
{
    public class GameCommandHandler : ICommandHandler<ICommand>, ICommandHandler<Open>, ICommandHandler<Close>
    {
        private IRepository repo;

        public GameCommandHandler(IRepository repo)
        {
            this.repo = repo;
        }

        public void Handle(string aggregateId, Open command)
        {
            Game newGame = repo.Restore(aggregateId) ?? new Game();
            command.Handle(newGame);
            repo.Save(newGame);
        }

        public void Handle(string aggregateId, Close command)
        {
            Game newGame = new Game();
            command.Handle(newGame);
            repo.Save(newGame);
        }

        public void Handle(string aggregateId, ICommand command)
        {
            switch (command)
            {
                case Open o:
                    this.Handle(aggregateId, o);
                    break;
                case Close c:
                    this.Handle(aggregateId, c);
                    break;

            }
        }
    }
}
