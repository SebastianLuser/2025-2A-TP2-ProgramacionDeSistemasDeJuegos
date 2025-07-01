using UnityEngine;

[CreateAssetMenu(fileName = "AnimationCommandLibrary", menuName = "Scriptable Objects/AnimationCommandLibrary")]
public class AnimationCommandLibrary : ScriptableObject
{
    public AnimationCommandConfig[] animations;
}
