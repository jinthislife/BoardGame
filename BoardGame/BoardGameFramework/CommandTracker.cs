using System.Collections.Generic;
using System;

namespace BoardGame
{
    public class CommandTracker
    {
        private Stack<ICommand> _Undoables = new Stack<ICommand>();
        private Stack<ICommand> _Redoables = new Stack<ICommand>();

        public void Undo()
        {
            if (_Undoables.Count > 0)
            {
                ICommand cmd = _Undoables.Pop();
                cmd.UnExecute();
                _Redoables.Push(cmd);
                return;
            }
            Console.WriteLine("No more undoable commands");
        }

        public void Redo()
        {
            if (_Redoables.Count > 0)
            {
                ICommand cmd = _Redoables.Pop();
                cmd.Execute();
                _Undoables.Push(cmd);
                return;
            }
            Console.WriteLine("No more redoable commands");
        }

        public void Insert(ICommand cmd)
        {
            _Undoables.Push(cmd);
            _Redoables.Clear();
        }

        public void Clear()
        {
            _Undoables.Clear();
            _Redoables.Clear();
        }
    }
}