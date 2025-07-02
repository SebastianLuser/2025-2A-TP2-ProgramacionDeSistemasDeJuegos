using System.Collections.Generic;

using UnityEngine;
public class CharacterFactory : ICharacterFactory
{
    private ICharacterSetup _currentSetup;

    public void Setup(ICharacterSetup setup)
    {
        _currentSetup = setup;
    }

    public Character CreateCharacter(Vector3 position, Quaternion rotation)
    {
        if (_currentSetup?.prefab == null) return null;

        var instance = Object.Instantiate(_currentSetup.prefab, position, rotation);

        ApplyCharacterSetups(instance, _currentSetup.characterSetups);
        ConfigureAnimator(instance, _currentSetup.animatorController);

        return instance.GetComponent<Character>();
    }

    private void ApplyCharacterSetups(GameObject instance, List<ScriptableObject> setups)
    {
        if (setups == null) return;

        foreach (var setupData in setups)
        {
            if (setupData == null) continue;

            ApplySpecificSetup(instance, setupData);
        }
    }

    private void ApplySpecificSetup(GameObject instance, ScriptableObject setupData)
    {
        switch (setupData)
        {
            case CharacterModelSetup characterSetup:
                SetupCharacterComponent(instance, characterSetup);
                break;
            case PlayerControllerModelSetup controllerSetup:
                SetupPlayerControllerComponent(instance, controllerSetup);
                break;
        }
    }

    private void SetupCharacterComponent(GameObject instance, CharacterModelSetup setup)
    {
        var character = instance.GetComponent<Character>();
        if (character == null)
            character = instance.AddComponent<Character>();

        setup.Setup(character);
    }

    private void SetupPlayerControllerComponent(GameObject instance, PlayerControllerModelSetup setup)
    {
        var controller = instance.GetComponent<PlayerController>();
        if (controller == null)
            controller = instance.AddComponent<PlayerController>();

        setup.Setup(controller);
    }
    private void ConfigureAnimator(GameObject instance, RuntimeAnimatorController controller)
    {
        if (controller == null) return;

        var animator = instance.GetComponentInChildren<Animator>();
        if (animator == null)
            animator = instance.AddComponent<Animator>();

        animator.runtimeAnimatorController = controller;
    }
}