using System;
namespace BoardGame
{
    interface ICommand // QQ move to separate file?
    {
        void Execute();
        void UnExecute();
    }

    public class PlaceCommand: ICommand
    {
        public PlaceCommand()
        {
        }

        void ICommand.Execute()
        {
            throw new NotImplementedException();
        }

        void ICommand.UnExecute()
        {
            throw new NotImplementedException();
        }
    }
}

