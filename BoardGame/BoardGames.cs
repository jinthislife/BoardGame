using System;
namespace BoardGame
{
    public enum BoardGames
    {
        TicTacToe = 1,
        Othello
    }

    public enum BoardGameState
    {
        Playing,
        Won,
        Draw,
    }
}