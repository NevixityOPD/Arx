using System;
using System.Collections.Generic;

namespace Arx.Internal.Command
{
    public class CommandManager
    {
        public List<ICommand> commands;

        public CommandManager() { commands = new List<ICommand>(); }

        public void Call(string cmdln)
        {
            string[] cmdlnSplit = cmdln.Split(' ');
            List<string> args = new List<string>();

            for (int i = 0; i < cmdlnSplit.Length; i++)
            {
                if (cmdlnSplit[i] != cmdlnSplit[0])
                {
                    args.Add(cmdlnSplit[i]);
                }
            }

            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].CommandName == cmdlnSplit[0]) { commands[i].CommandAction(args.ToArray()); }
            }
        }
    }
}
