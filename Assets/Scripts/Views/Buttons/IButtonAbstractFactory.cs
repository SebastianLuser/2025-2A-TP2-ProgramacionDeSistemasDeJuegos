using UnityEngine;
using UnityEngine.UI;

public interface IButtonAbstractFactory
{
    Button CreateButton(Transform parent);
}