using System;
using System.Collections.Generic;
using System.Linq;

namespace DebugConsole.Commands
{
    public class AliasesCommand : ICommand<string>
    {
        private readonly IDebugConsole<string> _console;

        public AliasesCommand(IDebugConsole<string> console) => _console = console;
        
        public string Name => "alias";

        public IEnumerable<string> Aliases => new[] { "aliases", "ALIAS", "ALIASES" };

        public string Description => "Logs the aliases for the given command";

        public void Execute(Action<string> log, params string[] args)
        {
            var cmdNameOrAlias = args[0];

            if (_console.IsValidCommand(cmdNameOrAlias))
            {
                var command = _console.Commands.FirstOrDefault(
                    c =>
                        c.Name == cmdNameOrAlias || c.Aliases.Contains(cmdNameOrAlias)
                );

                if (command != null)
                {
                    log($"{command.Name} => [{command.Aliases.Aggregate("", (current, alias) => $"{current}, {alias}")}]");
                }
            }

        }
    }
}