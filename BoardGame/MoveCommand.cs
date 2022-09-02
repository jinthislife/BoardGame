using System;
namespace BoardGame
{
    public class MoveCommand : ICommand
    {
        private Board board;
        private Move move;

        public MoveCommand(Move move, Board board)
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