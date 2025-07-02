using UnityEngine;

[CreateAssetMenu(fileName = "CharacterModelSetup", menuName = "Scriptable Objects/CharacterModelSetup")]
public class CharacterModelSetup : ScriptableObject, ISetup<Character>
{
    [SerializeField] private CharacterModel model;

    public void Setup(Character target)
    {
        target.Setup(model);
    }
}