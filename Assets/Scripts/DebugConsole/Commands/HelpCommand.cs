using System;
using System.Collections.Generic;
using System.Linq;

namespace DebugConsole.Commands
{
    public class HelpCommand : ICommand<string>
    {
        private readonly IDebugConsole<string> _console;

        public HelpCommand(IDebugConsole<string> console) => _console = console;

        public string Name => "help";

        public IEnumerable<string> Aliases => new[] { "h", "Help", "HELP", "?", "h?", "help?", "help!" };

        public string Description => "help <command>: Shows the command description.";

        public void Execute(Action<string> log, params string[] args)
        {
            if (args.Length > 0)
            {
                ShowSpecificCommandHelp(args[0], log);
                return;
            }

            ShowAllCommands(log);
        }

        private void ShowSpecificCommandHelp(string commandName, Action<string> log)
        {
            var command = FindCommand(commandName);
            
            if (command != null)
            {
                var aliasesText = command.Aliases.Any() ? $" ({string.Join(", ", command.Aliases)})" : "";
                log?.Invoke($"{command.Name}{aliasesText}: {command.Description}");
            }
            else
            {
                log?.Invoke($"Command '{commandName}' not found.");
            }
        }

        private void ShowAllCommands(Action<string> log)
        {
            log?.Invoke("Available Commands: \n");
            
            var commands = _console.Commands.OrderBy(c => c.Name).ToArray();
            
            for (int i = 0; i < commands.Length; i++)
            {
                var command = commands[i];
                log?.Invoke($"- {command.Name}: {command.Description}");
                
                if (i < commands.Length - 1)
                {
                    log?.Invoke("");
                }
            }
        }

        private ICommand<string> FindCommand(string commandName)
        {
            return _console.Commands.FirstOrDefault(c => 
                c.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase) || 
                c.Aliases.Any(alias => alias.Equals(commandName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}