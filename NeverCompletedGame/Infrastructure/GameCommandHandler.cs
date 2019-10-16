using System;
using System.Windows.Input;
using Playing;

namespace Infrastructure
{
    public class GameCommandHandler : ICommandHandler<Open>
    {
        private IRepository repo;

        public GameCommandHandler(IRepository repo)
        {
            this.repo = repo;
        }

        public void Handle(Open command)
        {
            Game newGame = new Game();
            command.Handle(newGame);
            repo.Save(newGame);
        }
    }
}
