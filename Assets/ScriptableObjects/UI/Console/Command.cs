using System;
using System.Collections.Generic;
using DebugConsole.Commands;
using UnityEngine;

[CreateAssetMenu(fileName = "Command", menuName = "Scriptable Objects/Command")]
public abstract class Command : ScriptableObject, ICommand<string>
{
    public abstract void Execute(Action<string> writeToConsole, params string[] args);
    public abstract string Name { get; }
    public abstract IEnumerable<string> Aliases { get; }
    public abstract string Description { get; }
}
