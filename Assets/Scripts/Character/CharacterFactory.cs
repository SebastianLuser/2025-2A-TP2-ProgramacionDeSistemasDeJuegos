using UnityEngine;

public class CharacterFactory : ICharacterAbstractFactory, ISetup<ICharacterSetup>
{
    public ICharacterSetup Model { get; set; }

    public void Setup(ICharacterSetup model)
    {
        Model = model;
    }

    public Character CreateCharacter(Vector3 position, Quaternion rotation)
    {
        var result = Object.Instantiate(Model.prefab, position, rotation);

        foreach (var setup in Model.characterSetups)
        {
            if (setup == null) continue;

            if (setup is ISetup<Character> characterSetup)
            {
                if (result.TryGetComponent(out Character character))
                    characterSetup.Setup(character);
                else
                {
                    character = result.AddComponent<Character>();
                    characterSetup.Setup(character);
                }
            }
            else if (setup is ISetup<PlayerController> controllerSetup)
            {
                if (result.TryGetComponent(out PlayerController controller))
                    controllerSetup.Setup(controller);
                else
                {
                    controller = result.AddComponent<PlayerController>();
                    controllerSetup.Setup(controller);
                }
            }
        }

        var animator = result.GetComponentInChildren<Animator>();
        if (!animator)
            animator = result.AddComponent<Animator>();
        animator.runtimeAnimatorController = Model.animatorController;

        return result.GetComponent<Character>();
    }
}