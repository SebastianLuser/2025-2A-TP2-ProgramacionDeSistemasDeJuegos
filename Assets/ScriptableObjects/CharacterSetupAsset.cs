using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSetupAsset", menuName = "Scriptable Objects/CharacterSetupAsset")]
public class CharacterSetupAsset : ScriptableObject, ICharacterSetup
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private List<ScriptableObject> _characterSetups;
    [SerializeField] private RuntimeAnimatorController _animatorController;

    public GameObject prefab => _prefab;
    public RuntimeAnimatorController animatorController => _animatorController;
    public List<ScriptableObject> characterSetups => _characterSetups;
}