using System.Collections.Generic;
using System;

namespace BoardGame
{
    public class MoveTracker
    {
        private Stack<PlaceCommand> _Undoables = new Stack<PlaceCommand>();
        private Stack<PlaceCommand> _Redoables = new Stack<PlaceCommand>();

        public void Undo()
        {
            if (_Undoables.Count > 0)
            {
                PlaceCommand place = _Undoables.Pop();
                place.UnExecute();
                _Redoables.Push(place);
                return;
            }
            Console.WriteLine("No more undoable commands");
        }

        public void Redo()
        {
            if (_Redoables.Count > 0)
            {
                PlaceCommand place = _Redoables.Pop();
                place.Execute();
                _Undoables.Push(place);
                return;
            }
            Console.WriteLine("No more redoable commands");
        }

        public void Insert(PlaceCommand place)
        {
            _Undoables.Push(place);
            _Redoables.Clear();
        }
    }
}