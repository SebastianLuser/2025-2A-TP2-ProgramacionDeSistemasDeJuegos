
using TMPro;
using Services;
using Spawning;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour, ISetup<ButtonSetupAsset.MenuButtons>
{
    [SerializeField] private Button button;
    private TextMeshProUGUI title;
    private ICharacterSetup _character;
    private ICharacterSpawner _spawner;

    private void Reset()
        => button = GetComponent<Button>();

    public void Setup(ButtonSetupAsset.MenuButtons menuButton)
    {
        title = GetComponentInChildren<TextMeshProUGUI>();
        if (title != null)
            title.text = menuButton.title;
        
        _character = menuButton.characterSetup;
        
        if (button != null)
            button.onClick.AddListener(HandleClick);
    }

    private void Awake()
    {
        if (!button)
            button = GetComponent<Button>();
    }

    private void Start()
    {
        _spawner = ServiceLocator.Get<ICharacterSpawner>();
    }
    
    private void OnEnable()
    {
        if (!button)
        {
            enabled = false;
        }
    }

    private void OnDisable()
    {
        button?.onClick?.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        if (_character == null)
        {
            return;
        }
        _spawner.Spawn(_character);
    }
}