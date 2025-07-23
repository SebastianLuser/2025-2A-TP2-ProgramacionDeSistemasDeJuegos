using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public IEnumerable<string> Aliases => new[] { "anim", "playanim", "play" };

        public string Description => BuildDescription();

        private string BuildDescription()
        {
            var sb = new StringBuilder();
            sb.Append("playanimation <animation>: Play an animation in all the characters.");
            
            if (HasAnimations())
            {
                var animations = GetUniqueAnimations();
                sb.AppendLine(" Available Animations:");
                sb.Append(string.Join("\n", animations.Select(name => $"- {name}")));
            }
            
            return sb.ToString();
        }

        private bool HasAnimations()
        {
            return _animationLibrary?.animations != null && _animationLibrary.animations.Length > 0;
        }

        private IEnumerable<string> GetUniqueAnimations()
        {
            return _animationLibrary.animations
                .Select(a => a.commandName)
                .Distinct()
                .OrderBy(name => name);
        }

        public void Execute(Action<string> log, params string[] args)
        {
            if (args.Length == 0)
            {
                ShowAvailableAnimations(log);
                return;
            }
            
            ExecuteAnimation(args[0]);
        }

        private void ShowAvailableAnimations(Action<string> log)
        {
            if (!HasAnimations()) return;

            foreach (var animationName in GetUniqueAnimations())
                log($"- {animationName}");
        }

        private void ExecuteAnimation(string animationName)
        {
            if (!HasAnimations()) return;
            
            var animCommand = _animationLibrary.animations.FirstOrDefault(c => c.commandName == animationName);
            if (animCommand == null) return;
            
            var animators = CharacterAnimatorRegistry.GetActive();
            foreach (var charAnim in animators)
            {
                charAnim.ForceAnimation(animCommand, 2f);
            }
        }
    }
}