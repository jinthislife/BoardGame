using System;
namespace BoardGame
{
    public interface ICommand
    {
        public void Execute();
        //public void UnExecute();
    }

}