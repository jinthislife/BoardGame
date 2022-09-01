using System;
using System.Linq;

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

        public override HelpSystem CreateHelpSystem()
        {
            return new TicTacToeHelpSystem();
        }

        public override Piece CreatePiece()
        {
            Char[] availableSymbols = SymbolPiece.GetAvailable();

            if (availableSymbols.Length == 0) {
                throw new ArgumentOutOfRangeException();
            }

            return new SymbolPiece(symbol: availableSymbols[0]);
        }

        public override EasyMoveStrategy CreateEasyMoveStrategy(Board board)
        {
            return new TicTacToeEasyMoveStrategy(board);
        }

        public override NormalMoveStrategy CreateNormalMoveStrategy(Board board)
        {
            return new TicTacToeNormalMoveStrategy(board);
        }

        public override Player[] CreatePlayers(int mode, AIMoveStrategy strategy)
        {
            Player[] players = new Player[2];
            players[0] = new HumanPlayer(Id: 1, piece: CreatePiece());

            if (mode == 1)
            {
                players[1] = new HumanPlayer(Id: 2, piece: CreatePiece());
            }
            else
            {
                players[1] = new AIPlayer(Id: 2, piece: CreatePiece(), strategy: strategy);
            }

            Console.Write($"\n{players[0].ToString()} got '{players[0].Piece.ToString()}'. ");
            Console.WriteLine($"{players[1].ToString()} got '{players[1].Piece.ToString()}'.");
            return players;
        }
    }
}