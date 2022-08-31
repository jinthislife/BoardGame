using System;
namespace BoardGame
{
    public class LoadCommand : ICommand
    {
        private Storage _storage;
        private Board _board;

        public LoadCommand(Storage storage, Board board)
        {
            this._storage = storage;
            this._board = board;
        }

        public void Execute()
        {
            _board.LoadStates(_storage.Load());
        }
    }
}