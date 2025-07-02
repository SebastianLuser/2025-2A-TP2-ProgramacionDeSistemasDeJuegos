using System;
using TMPro;
using Services;
using Spawning;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour, ISetup<ButtonSetupAsset>
{
    [SerializeField] private Button button;
    private TextMeshProUGUI title;
    private ICharacterSetup _character;
    private ICharacterSpawner _spawner;

    private void Reset()
        => button = GetComponent<Button>();

    public void Setup(ButtonSetupAsset model)
    {
        title = GetComponentInChildren<TextMeshProUGUI>();
        title.text = model.text;
        _character = model.characterSetup;
        
        button.onClick.AddListener(HandleClick);
    }

    private void Awake()
    {
        if (!button)
            button = GetComponent<Button>();
        
        _spawner = ServiceLocator.Get<ICharacterSpawner>();
    }

    private void OnEnable()
    {
        if (!button)
        {
            enabled = false;
            return;
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