using Services;
using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [SerializeField] private Transform buttonLayout;
    [SerializeField] private ButtonSetupAsset buttonSetupAsset;
    [SerializeField] private GameObject buttonPrefab;

    private IButtonAbstractFactory _buttonAbstractFactory;

    private void Start()
    {
        _buttonAbstractFactory = ServiceLocator.Get<IButtonAbstractFactory>();
        BuildMenu();
    }

    private void BuildMenu()
    {
        ClearExistingButtons();

        if (buttonSetupAsset?.buttons == null) return;

        foreach (var buttonConfig in buttonSetupAsset.buttons)
        {
            if (buttonConfig?.characterSetup == null) continue;

            CreateButton(buttonConfig);
        }
    }

    private void ClearExistingButtons()
    {
        foreach (Transform child in buttonLayout)
            Destroy(child.gameObject);
    }

    private void CreateButton(ButtonSetupAsset.MenuButtons buttonConfig)
    {
        var factory = _buttonAbstractFactory.GetFactory(buttonConfig);
        var button = factory.CreateButton(buttonLayout, null);
    
        var spawnButton = button.GetComponent<SpawnButton>();
        if (spawnButton == null)
            spawnButton = button.gameObject.AddComponent<SpawnButton>();
        
        spawnButton.Setup(buttonConfig);
    }

}