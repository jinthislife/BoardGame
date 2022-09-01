using System;
using System.ComponentModel;

namespace BoardGame
{
    public abstract class GameFactory
    {
        public abstract Board CreateBoard();
        public abstract Storage CreateStorage();
        public abstract Piece CreatePiece();
        public abstract AIMoveStrategy CreateEasyMoveStrategy(Board board);
        public abstract AIMoveStrategy CreateNormalMoveStrategy(Board board);
        public abstract HelpSystem CreateHelpSystem();
        public abstract Player[] CreatePlayers(int mode, AIMoveStrategy strategy);
    }
}