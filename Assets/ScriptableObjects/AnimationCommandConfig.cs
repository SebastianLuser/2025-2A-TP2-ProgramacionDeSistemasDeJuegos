using UnityEngine;

[CreateAssetMenu(fileName = "AnimationCommandConfig", menuName = "Scriptable Objects/AnimationCommandConfig")]
public class AnimationCommandConfig : ScriptableObject
{
    public string commandName;
    public string animatorStateName;
    public AnimatorParameter[] parameters;

    [System.Serializable]
    public class AnimatorParameter
    {
        public string name;
        public AnimatorControllerParameterType type;
        public float floatValue;
        public bool boolValue;
    }
}
