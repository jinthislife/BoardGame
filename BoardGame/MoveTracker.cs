using System.Collections.Generic;
using System;

namespace BoardGame
{
    public struct MoveRecord
    {
        public Move latest;
        public Move[,] moves; // QQ accessibility

        public MoveRecord(Move move, Move[,] moves)
        {
            this.latest = move;
            this.moves = moves;
        }
    }

    public class MoveTracker
    {
        private Stack<PlaceCommand> _Undoables = new Stack<PlaceCommand>();
        private Stack<PlaceCommand> _Redoables = new Stack<PlaceCommand>();

        protected Move[,] moves; // rename to state
 

        public MoveTracker()
        {
            moves = new Move[3,3];
        }

        public void Undo()
        {
            if (_Undoables.Count > 0)
            {
                PlaceCommand place = _Undoables.Pop();
                place.UnExecute();
                _Redoables.Push(place);
            }
        }

        public void Redo()
        {
            if (_Redoables.Count > 0)
            {
                PlaceCommand place = _Redoables.Pop();
                place.Execute();
                _Undoables.Push(place);
            }
        }

        public void InsertMove(PlaceCommand place)
        {
            _Undoables.Push(place);
            _Redoables.Clear();
        }

    }
}