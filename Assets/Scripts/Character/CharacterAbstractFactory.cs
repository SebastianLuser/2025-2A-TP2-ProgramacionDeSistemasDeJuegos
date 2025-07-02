using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbstractFactory : ICharacterAbstractFactory
{
    private readonly IFactoryRegistry _registry;
    private readonly ICharacterFactory _defaultFactory;

    public CharacterAbstractFactory(IFactoryRegistry registry, ICharacterFactory defaultFactory)
    {
        _registry = registry;
        _defaultFactory = defaultFactory;
    }

    public ICharacterFactory GetFactory(ICharacterSetup setup)
    {
        var specificFactory = GetSpecificFactory(setup);
        if (specificFactory != null)
        {
            specificFactory.Setup(setup);
            return specificFactory;
        }

        _defaultFactory.Setup(setup);
        return _defaultFactory;
    }

    private ICharacterFactory GetSpecificFactory(ICharacterSetup setup)
    {
        var setupType = setup.GetType();

        var factoryMethod = typeof(IFactoryRegistry).GetMethod(nameof(IFactoryRegistry.GetCharacterFactory));
        var genericMethod = factoryMethod?.MakeGenericMethod(setupType);

        try
        {
            return genericMethod?.Invoke(_registry, null) as ICharacterFactory;
        }
        catch
        {
            return null;
        }
    }
}