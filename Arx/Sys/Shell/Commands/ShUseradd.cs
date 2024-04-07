using Arx.Sys.Stdio;
using System;
using System.Collections.Generic;

namespace Arx.Sys.Shell.Commands
{
    public class ShUseradd : ShellCommand
    {
        public ShUseradd() : base("useradd", "Add new user") { }

        public override void Execute(string[] args)
        {
            int usernameAttempt = 3;

            ReEnterUsername:
            string username = Read.Prompt("User Add", ConsoleColor.Green, "Enter username");
            if(string.IsNullOrEmpty(username))
            {
                if(usernameAttempt == 0)
                {
                    Write.Println("Failed to add new user!", ConsoleColor.Red);
                    return;
                }
                else
                {
                    usernameAttempt--;
                    Write.Println("Username can't be empty!", ConsoleColor.Red);
                    goto ReEnterUsername;
                }
            }
            string password = Read.Prompt("User Add", ConsoleColor.Green, "Enter password (Optional)");

            bool isRoot = Read.BoolPrompt("User Add", ConsoleColor.Green, "Create user as root");

            if(isRoot) { Kernel.UserManager.CreateUser(new User.User(username, password, User.UserAccess.Root)); }
            else { Kernel.UserManager.CreateUser(new User.User(username, password, User.UserAccess.User)); }
        }
    }
}
