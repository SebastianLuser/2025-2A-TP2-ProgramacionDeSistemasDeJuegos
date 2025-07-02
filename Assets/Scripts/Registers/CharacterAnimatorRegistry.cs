using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CharacterAnimatorRegistry
{
    private static readonly HashSet<CharacterAnimator> _animators = new();

    public static void Register(CharacterAnimator animator)
    {
        _animators.Add(animator);
    }

    public static void Unregister(CharacterAnimator animator)
    {
        _animators.Remove(animator);
    }

    public static IEnumerable<CharacterAnimator> GetActive()
    {
        return _animators.Where(a => a != null && a.gameObject.activeInHierarchy);
    }

    public static int Count => _animators.Count;
}