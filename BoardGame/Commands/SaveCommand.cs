using System;
namespace BoardGame
{
    public class SaveCommand : ICommand
    {
        Storage _storage;
        Board _board;

        public SaveCommand(Storage storage, Board board)
        {
            this._storage = storage;
            this._board = board;
        }

        public void Execute()
        {
            _storage.save(_board.getStates());
        }
    }
}

