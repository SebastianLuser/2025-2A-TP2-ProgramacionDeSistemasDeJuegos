using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFactory : IButtonAbstractFactory, ISetup<IButtonSetup>
{
    public IButtonSetup Model { get; set; }

    public void Setup(IButtonSetup model)
    {
        Model = model;
    }

    public Button CreateButton(Transform parent)
    {
        var buttonInstance = Object.Instantiate(Model.buttonPrefab, parent);
        
        var text = buttonInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
            text.text = Model.buttonTitle;


        return buttonInstance.GetComponent<Button>();
    }
}