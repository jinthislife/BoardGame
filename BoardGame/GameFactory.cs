using System;
using System.ComponentModel;

namespace BoardGame
{
    public abstract class GameFactory
    {
        public abstract Board CreateBoard();
        public abstract Storage CreateStorage();
        public abstract HelpSystem CreateHelpSystem();
        public abstract Piece CreatePiece();
    }

}