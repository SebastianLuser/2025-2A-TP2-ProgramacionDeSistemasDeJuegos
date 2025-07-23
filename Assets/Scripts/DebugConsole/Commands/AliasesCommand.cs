using System;
using System.Collections.Generic;
using System.Linq;

namespace DebugConsole.Commands
{
    public class AliasesCommand : ICommand<string>
    {
        private readonly IDebugConsole<string> _console;

        public AliasesCommand(IDebugConsole<string> console) => _console = console;
        
        public string Name => "aliases";

        public IEnumerable<string> Aliases => new[] { "alias", "ALIAS", "ALIASES" };

        public string Description => "aliases <command>: Shows a command alias.";

        public void Execute(Action<string> log, params string[] args)
        {
            if (args.Length == 0)
            {
                log?.Invoke("Usage: aliases <command>");
                return;
            }

            var commandName = args[0];
            var command = FindCommand(commandName);

            if (command != null)
            {
                ShowCommandAliases(command, log);
            }
            else
            {
                log?.Invoke($"Command '{commandName}' not found.");
            }
        }

        private ICommand<string> FindCommand(string commandName)
        {
            return _console.Commands.FirstOrDefault(c => 
                c.Name == commandName || c.Aliases.Contains(commandName));
        }

        private void ShowCommandAliases(ICommand<string> command, Action<string> log)
        {
            var aliasesText = string.Join(", ", command.Aliases);
            log?.Invoke($"{command.Name} => [{aliasesText}]");
        }
    }
}