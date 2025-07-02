using Services;
using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [SerializeField] private Transform buttonLayout;
    [SerializeField] private ButtonSetupAsset buttonSetupAsset;
    [SerializeField] private GameObject buttonPrefab;

    private void Start()
    {
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
        var buttonInstance = Instantiate(buttonPrefab, buttonLayout);
        
        var spawnButton = buttonInstance.GetComponent<SpawnButton>();
        if (spawnButton == null)
            spawnButton = buttonInstance.AddComponent<SpawnButton>();
            
        spawnButton.Setup(buttonConfig);
    }
}