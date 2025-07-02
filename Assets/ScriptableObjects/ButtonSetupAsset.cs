using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonSetupAsset", menuName = "Scriptable Objects/ButtonSetupAsset")]
public class ButtonSetupAsset : ScriptableObject
{
    [SerializeField] private string _text;
    [SerializeField] private CharacterSetupAsset _characterSetup;

    public string text => _text;
    public CharacterSetupAsset characterSetup => _characterSetup;
}