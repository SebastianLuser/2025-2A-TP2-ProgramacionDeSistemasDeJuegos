using System;
using System.Collections.Generic;

namespace DebugConsole.Commands
{
    public interface ICommand<T>
    {
        T Name { get; }
        IEnumerable<T> Aliases { get; }
        T Description { get; }
        void Execute(Action<T> log, params T[] args);
    }
}