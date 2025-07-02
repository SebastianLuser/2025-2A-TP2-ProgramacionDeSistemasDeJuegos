using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonSetupAsset", menuName = "Scriptable Objects/ButtonSetupAsset")]
public class ButtonSetupAsset : ScriptableObject
{
    public List<MenuButtons> buttons = new List<MenuButtons>();

    [Serializable]
    public class MenuButtons
    {
        public string title;
        public CharacterSetupAsset characterSetup;
    }
}