using UnityEngine;

public interface ICharacterFactory : ISetup<ICharacterSetup>
{
    void CreateCharacter(Vector3 position, Quaternion rotation);
}