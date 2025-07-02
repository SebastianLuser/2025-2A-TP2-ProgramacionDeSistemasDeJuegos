using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFactory : IButtonFactory
{
    private readonly GameObject _buttonPrefab;
    private object _currentConfig;

    public ButtonFactory(GameObject buttonPrefab)
    {
        _buttonPrefab = buttonPrefab;
    }

    public void Setup(object config)
    {
        _currentConfig = config;
    }

    public Button CreateButton(Transform parent, Action<object> onClick)
    {
        if (_currentConfig == null || _buttonPrefab == null) return null;

        var buttonInstance = UnityEngine.Object.Instantiate(_buttonPrefab, parent);
        
        ConfigureButtonText(buttonInstance);
        ConfigureButtonAction(buttonInstance, onClick);
        
        return buttonInstance.GetComponent<Button>();
    }

    private void ConfigureButtonText(GameObject buttonInstance)
    {
        var text = buttonInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (text == null) return;

        var title = GetButtonTitle();
        if (!string.IsNullOrEmpty(title))
            text.text = title;
    }

    private string GetButtonTitle()
    {
        return _currentConfig switch
        {
            ButtonSetupAsset.MenuButtons menuButton => menuButton.title,
            _ => "Button"
        };
    }

    private void ConfigureButtonAction(GameObject buttonInstance, Action<object> onClick)
    {
        var button = buttonInstance.GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(() => onClick?.Invoke(_currentConfig));
    }
}