using System;
namespace BoardGame
{
    public class PlaceCommand : ICommand
    {
        private Board board;
        private Move move;

        public PlaceCommand(Move move, Board board)
        {
            this.move = move;
            this.board = board;
        }

        public void Execute()
        {
            board.Place(move);
        }

        public void UnExecute()
        {
            board.Withdraw(move);
        }
    }

}

