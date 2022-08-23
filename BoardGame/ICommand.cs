using System;
namespace BoardGame
{
    public interface ICommand
    {
        void Execute();

        public void UnExecute()
        {
            // Empty default implementation
            // As UnExecute() is necessary for undoable command like Place
            // other commands not tracked do not need to implement this
        }
    }

}

