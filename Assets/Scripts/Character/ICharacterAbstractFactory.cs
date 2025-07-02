using UnityEngine;

public interface ICharacterAbstractFactory
{
    ICharacterFactory GetFactory(ICharacterSetup setup);
}