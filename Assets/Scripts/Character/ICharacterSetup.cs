using System.Collections.Generic;
using UnityEngine;

public interface ICharacterSetup
{
    GameObject prefab { get; }
    List<ScriptableObject> characterSetups { get; }
    RuntimeAnimatorController animatorController { get; }
}