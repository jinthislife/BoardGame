using System;
namespace BoardGame
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }

}

