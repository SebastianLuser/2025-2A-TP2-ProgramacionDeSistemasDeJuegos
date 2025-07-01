using System;
using System.Collections.Generic;
using System.Linq;
using DebugConsole.Commands;
using UnityEngine;

namespace DebugConsole
{
    [CreateAssetMenu(menuName = "Debug Console/Debug Console", fileName = "DebugConsole", order = 1000)]
    public class ConsoleWrapper : ScriptableObject, IDebugConsole<string>
    {
        [SerializeField] protected List<CommandSO> commands;
        [SerializeField] protected char[] separators;
        protected IDebugConsole<string> DebugConsole;
        public Action<string> log = delegate { };

        protected void OnEnable()
        {
            DebugConsole = new DebugConsole<string>((str) => log(str), commands.Cast<ICommand<string>>().ToArray());
            var aliasesCommand = new AliasesCommand(DebugConsole);
            DebugConsole.AddCommand(aliasesCommand);
    
            var helpCommand = new HelpCommand(DebugConsole);
            DebugConsole.AddCommand(helpCommand);
        }

        public HashSet<ICommand<string>> Commands
        {
            get => DebugConsole.Commands;
            set => DebugConsole.Commands = value;
        }

        public void AddCommand(ICommand<string> command)
        {
            DebugConsole.AddCommand(command);
        }
        
        public bool IsValidCommand(string name)
            => DebugConsole.IsValidCommand(name);

        public void ExecuteCommand(string name, params string[] args)
        {
            DebugConsole.ExecuteCommand(name,args);
        }

        public bool TryAddCommand(ICommand<string> command)
            => DebugConsole.TryAddCommand(command);
        
        public bool TryUseInput(string input)
        {
            var inputs = input.Split(separators);
            var commandName = inputs[0];
            if (!DebugConsole.IsValidCommand(commandName))
            {
                return false;
            }

            var args = inputs[1..];

            DebugConsole.ExecuteCommand(commandName, args);
            return true;
        }

    }
}