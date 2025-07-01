using System;
using System.Collections.Generic;
using System.Linq;

namespace DebugConsole.Commands
{
    public class PlayAnimationCommand : ICommand<string>
    {
        private readonly IDebugConsole<string> _console;
        private readonly AnimationCommandLibrary _animationLibrary;
        
        public PlayAnimationCommand(IDebugConsole<string> console, AnimationCommandLibrary animationLibrary)
        {
            _console = console;
            _animationLibrary = animationLibrary;
        }
        
        public string Name => "playanimation";

        public IEnumerable<string> Aliases => new[] { "anim", "playanim", "pa" };

        public string Description => "Play an animation in all the characters";

        public void Execute(Action<string> log, params string[] args)
        {
            if (args.Length == 0)
            {
                if (_animationLibrary?.animations != null)
                {
                    foreach (var a in _animationLibrary.animations)
                        log($"- {a.commandName}");
                }
                return;
            }
            
            var cmdName = args[0];
            
            if (_animationLibrary?.animations == null)
            {
                return;
            }
            
            var animCommand = _animationLibrary.animations.FirstOrDefault(c => c.commandName == cmdName);
            if (animCommand == null)
            {
                return;
            }
            
            var animators = CharacterAnimatorRegistry.GetActive();
            
            int count = 0;
            foreach (var charAnim in animators)
            {
                charAnim.ForceAnimation(animCommand, 2f);
                count++;
            }
        }
    }
}