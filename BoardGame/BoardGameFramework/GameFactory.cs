using System;
using System.ComponentModel;

namespace BoardGame
{
    public abstract class GameFactory
    {
        public abstract Board CreateBoard();
        public abstract Storage CreateStorage();
        public abstract Piece CreatePiece();
        public abstract MoveStrategy CreateEasyMoveStrategy(Board board);
        public abstract MoveStrategy CreateNormalMoveStrategy(Board board);
        public abstract HelpSystem CreateHelpSystem();
        public abstract Player[] CreatePlayers(int mode, MoveStrategy strategy);
    }
}