using System;
using UnityEngine;
using UnityEngine.UI;

public interface IButtonFactory : ISetup<object>
{
    Button CreateButton(Transform parent, Action<object> onClick);
}