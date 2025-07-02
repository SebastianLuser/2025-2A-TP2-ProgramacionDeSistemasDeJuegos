using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [SerializeField] private Transform buttonLayout;
    [SerializeField] private ButtonSetupAsset[] buttonConfigs;
    [SerializeField] private GameObject buttonPrefab;
    
    private IButtonAbstractFactory buttonFactory;

    private void Start()
    {
        buttonFactory = new ButtonFactory();
        BuildMenu();
    }

    private void BuildMenu()
    {
        foreach (Transform child in buttonLayout)
            Destroy(child.gameObject);

        foreach (var buttonConfig in buttonConfigs)
        {
            var buttonInstance = Instantiate(buttonPrefab, buttonLayout);
            
            var spawnButton = buttonInstance.GetComponent<SpawnButton>();
            if (spawnButton != null)
            {
                spawnButton.Setup(buttonConfig);
            }
        }
    }
}