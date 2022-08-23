using System;
namespace BoardGame
{
    public class PlaceCommand : ICommand
    {
        private MoveTracker moveTracker;
        private Board board;
        private Move move;

        public PlaceCommand(Move move, MoveTracker moveTracker, Board board)
        {
            this.move = move;
            this.moveTracker = moveTracker;
            this.board = board;
        }

        public void Execute()
        {
            board.placeMove(move);
            moveTracker.InsertMove(this);
        }

        public void UnExecute()
        {
            board.withdrawMove(move);
        }
    }

}

