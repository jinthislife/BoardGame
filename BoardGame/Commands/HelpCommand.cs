using System;
namespace BoardGame
{
    public class HelpCommand : ICommand
    {
        HelpSystem _helpSystem;
        public HelpCommand(HelpSystem helpSystem)
        {
            this._helpSystem = helpSystem;
        }

        public void Execute()
        {
            _helpSystem.displayManual();
        }
    }
}

