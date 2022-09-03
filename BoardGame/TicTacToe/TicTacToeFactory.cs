using System;

namespace BoardGame
{
    public class TicTacToeFactory : GameFactory
    {
        public override Board CreateBoard()
        {
            return new TicTacToeBoard(width: 3, height: 3);
        }

        public override Storage CreateStorage()
        {
            return new TicTacToeStorage();
        }

        public override Piece CreatePiece()
        {
            Char[] availableSymbols = TicTacToePiece.GetAvailable();

            if (availableSymbols.Length == 0) {
                throw new ArgumentOutOfRangeException();
            }

            return new TicTacToePiece(symbol: availableSymbols[0]);
        }

        public override MoveStrategy CreateEasyMoveStrategy(Board board)
        {
            return new TicTacToeEasyMoveStrategy(board);
        }

        public override MoveStrategy CreateNormalMoveStrategy(Board board)
        {
            return new TicTacToeNormalMoveStrategy(board);
        }
    }
}