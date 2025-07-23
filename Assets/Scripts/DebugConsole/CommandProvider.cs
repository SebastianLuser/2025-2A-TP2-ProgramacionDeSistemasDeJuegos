using System.Collections.Generic;
using DebugConsole;
using DebugConsole.Commands;
using Services;
using UnityEngine;

public class CommandProvider : MonoBehaviour
{
    [SerializeField] private AnimationCommandLibrary animationLibrary;
    
    private void Awake()
    {
        ServiceLocator.Register<CommandProvider>(this);
    }
    
    private void OnDestroy()
    {
        ServiceLocator.Unregister<CommandProvider>();
    }
    
    public IEnumerable<ICommand<string>> CreateCommands(IDebugConsole<string> console)
    {
        return new List<ICommand<string>>
        {
            new AliasesCommand(console),
            new HelpCommand(console),
            new PlayAnimationCommand(console, animationLibrary)
        };
    }
}