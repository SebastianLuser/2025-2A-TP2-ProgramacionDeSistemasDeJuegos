using System;
using System.Collections.Generic;
using System.Linq;
using DebugConsole.Commands;
using UnityEngine;

namespace DebugConsole
{
    [CreateAssetMenu(menuName = "Debug Console/Debug Console", fileName = "DebugConsole", order = 1000)]
    public class ConsoleWrapper : ScriptableObject, IDebugConsole<string>, ILogHandler
    {
        [SerializeField] protected List<Command> commands;
        [SerializeField] protected char[] separators;
        [SerializeField] protected AnimationCommandLibrary animationLibrary;
        
        protected IDebugConsole<string> DebugConsole;
        private ILogHandler _originalLogHandler;
        
        public Action<string> log = delegate { };

        protected void OnEnable()
        {
            DebugConsole = new DebugConsole<string>((str) => log(str), commands.Cast<ICommand<string>>().ToArray());
            
            var aliasesCommand = new AliasesCommand(DebugConsole);
            DebugConsole.AddCommand(aliasesCommand);
    
            var helpCommand = new HelpCommand(DebugConsole);
            DebugConsole.AddCommand(helpCommand);
            
            var playAnimationCommand = new PlayAnimationCommand(DebugConsole, animationLibrary);
            DebugConsole.AddCommand(playAnimationCommand);
            
            _originalLogHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = this;
        }
        
        private void OnDisable()
        {
            if (_originalLogHandler != null)
            {
                Debug.unityLogger.logHandler = _originalLogHandler;
            }
        }
        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            var message = string.Format(format, args);
            var formattedMessage = FormatLogMessage(logType, message);
            
            log(formattedMessage);
            
            _originalLogHandler?.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            var message = $"<color=red>Exception: {exception.Message}</color>";
            log(message);
            
            _originalLogHandler?.LogException(exception, context);
        }

        private string FormatLogMessage(LogType logType, string message)
        {
            return logType switch
            {
                LogType.Error => $"<color=red>[ERROR] {message}</color>",
                LogType.Warning => $"<color=yellow>[WARNING] {message}</color>",
                LogType.Log => $"[LOG] {message}",
                LogType.Exception => $"<color=red>[EXCEPTION] {message}</color>",
                LogType.Assert => $"<color=orange>[ASSERT] {message}</color>",
                _ => message
            };
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
            DebugConsole.ExecuteCommand(name, args);
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