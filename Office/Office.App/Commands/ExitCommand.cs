using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;

namespace Office.App.Commands
{
    public class ExitCommand : ICommand
    {
        private const string InvalidLength = "Invalid arguments count!";

        public string Execute(string[] data)
        {
            if (!Validation.CheckLength(0, data))
            {
                return InvalidLength;
            }
            Environment.Exit(0);

            return "Bye-bye!";
        }
    }
}
