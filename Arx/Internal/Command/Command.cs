using System;

namespace Arx.Internal.Command
{
    public interface ICommand
    {
        string CommandName { get; }
        string CommandDescription { get; }
        Action CommandAction(string[] args);
    }
}
