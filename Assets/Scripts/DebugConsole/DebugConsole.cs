using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DebugConsole.Commands;

namespace DebugConsole
{
    public class DebugConsole<T> : IDebugConsole<T>
    {
        private readonly Action<T> _log;
        private readonly Dictionary<T, ICommand<T>> _commandDictionary;

        public DebugConsole(Action<T> log, params ICommand<T>[] commands)
        {
            _log = log;
            Commands = commands.ToHashSet();
            _commandDictionary = new Dictionary<T, ICommand<T>>();
            foreach (var command in Commands)
                AddToCommandDictionary(command);
        }

        public HashSet<ICommand<T>> Commands { get; set; }

        public void AddCommand(ICommand<T> command)
        {
            if (!Commands.Add(command))
                throw new DuplicateNameException($"Command {command.Name} has already been added");
            AddToCommandDictionary(command);
        }

        public bool TryAddCommand(ICommand<T> command)
        {
            if (Commands.Contains(command)
                || !TryAddToCommandDictionary(command))
                return false;

            Commands.Add(command);
            return true;
        }
        
        public bool IsValidCommand(T name) => _commandDictionary.ContainsKey(name);
        
        public void ExecuteCommand(T name, params T[] args)
        {
            if (!IsValidCommand(name))
                return;

            _commandDictionary[name].Execute(_log, args);
        }

        private void AddToCommandDictionary(ICommand<T> command)
        {
            if (!_commandDictionary.ContainsKey(command.Name))
                _commandDictionary.Add(command.Name, command);
            else
                throw new DuplicateNameException($"Command {command.Name} already exists in commands dictionary");

            foreach (var alias in command.Aliases)
            {
                if (_commandDictionary.TryGetValue(alias, out var duplicate))
                    throw new DuplicateNameException($"A command with alias: {alias} already exists in commands dictionary" +
                                                     $"\n{alias} -> {duplicate.Name}");

                _commandDictionary.Add(alias, command);
            }
        }

        private bool TryAddToCommandDictionary(ICommand<T> command)
        {
            if (!_commandDictionary.ContainsKey(command.Name))
            {
                _commandDictionary.Add(command.Name, command);
                return true;
            }
            foreach (var alias in command.Aliases)
            {
                if (!_commandDictionary.ContainsKey(alias))
                {
                    _commandDictionary.Add(alias, command);
                    return true;
                }
            }

            return false;
        }
    }
}