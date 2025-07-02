using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerModelSetup", menuName = "Scriptable Objects/PlayerControllerModelSetup")]
public class PlayerControllerModelSetup : ScriptableObject, ISetup<PlayerController>
{
    [SerializeField] private PlayerControllerModel model;

    public void Setup(PlayerController target)
    {
        target.Setup(model);
    }
}
