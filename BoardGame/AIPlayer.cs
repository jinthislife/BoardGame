using System;
namespace BoardGame
{
    public class AIPlayer : Player
    {
        public AIPlayer(Piece piece, string name = "AI") : base(name, piece)
        {
        }

        public override string generateCommand()
        {//is it necessary?
            return "";
        }

    }
}

