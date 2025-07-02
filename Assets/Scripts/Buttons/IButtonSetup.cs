using System.Collections.Generic;
using UnityEngine;

public interface IButtonSetup
{
    GameObject buttonPrefab { get; }
    List<ScriptableObject> buttonSetups { get; }
    CharacterSetupAsset characterToSpawn { get; }
    string buttonTitle { get; }
}