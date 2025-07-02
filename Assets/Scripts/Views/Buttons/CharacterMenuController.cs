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
        foreach (Transform child in buttonLayout)
            Destroy(child.gameObject);

        if (buttonSetupAsset == null || buttonSetupAsset.buttons == null)
            return;

        foreach (var buttonConfig in buttonSetupAsset.buttons)
        {
            if (buttonConfig == null || buttonConfig.characterSetup == null)
                continue;

            var buttonInstance = Instantiate(buttonPrefab, buttonLayout);
            
            var spawnButton = buttonInstance.GetComponent<SpawnButton>();
            if (spawnButton != null)
            {
                spawnButton.Setup(buttonConfig);
            }
        }
    }
}