using UnityEngine;

public interface ICharacterAbstractFactory
{
    Character CreateCharacter(Vector3 position, Quaternion rotation);
}