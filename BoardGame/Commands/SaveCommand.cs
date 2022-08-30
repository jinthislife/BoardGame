using System;
namespace BoardGame
{
    public class SaveCommand : ICommand
    {
        private Storage _storage;
        private Board _board;

        public SaveCommand(Storage storage, Board board)
        {
            this._storage = storage;
            this._board = board;
        }

        public void Execute()
        {
            _storage.Save(_board.GetStates());
        }
    }
}
