using UnityEngine;

    public interface ICharacterFactory : ISetup<ICharacterSetup>
    {
        Character CreateCharacter(Vector3 position, Quaternion rotation);
    }