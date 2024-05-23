using Arx.Sys.User;
using System;
using System.Threading.Tasks;

namespace Arx.Sys.Shell
{
    public abstract class ShellCommand
    {
        public ShellCommand(string shellName, string shellDesc, UserAccess shellAccess) { this.shellName = shellName; this.shellDesc = shellDesc; this.shellAccess = shellAccess; }

        public string shellName { get; }
        public string shellDesc { get; }
        public UserAccess shellAccess { get; }

        public abstract void Execute(string[] args);
    }
}
