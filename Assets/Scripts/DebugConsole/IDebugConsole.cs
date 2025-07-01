using System.Collections.Generic;
using DebugConsole.Commands;

namespace DebugConsole
{
    public interface IDebugConsole<T>
    {
        HashSet<ICommand<T>> Commands { get; set; }
        void AddCommand(ICommand<T> command);
        bool IsValidCommand(T name);
        void ExecuteCommand(T name, params T[] args);
        bool TryAddCommand(ICommand<T> command);
    }
}