using System;
using System.Threading.Tasks;

namespace Arx.Sys.Shell
{
    public abstract class ShellCommand
    {
        public ShellCommand(string shellName, string shellDesc) { this.shellName = shellName; this.shellDesc = shellDesc; }

        public string shellName { get; }
        public string shellDesc { get; }

        public abstract void Execute(string[] args);
    }
}
