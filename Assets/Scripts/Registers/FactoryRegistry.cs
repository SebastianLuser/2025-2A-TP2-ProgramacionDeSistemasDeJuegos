using System;
using System.Collections.Generic;

public class FactoryRegistry : IFactoryRegistry
{
    private readonly Dictionary<Type, ICharacterFactory> _characterFactories = new();
    private IButtonFactory _buttonFactory;

    public void RegisterCharacterFactory<T>(ICharacterFactory factory) where T : ICharacterSetup
    {
        _characterFactories[typeof(T)] = factory;
    }

    public void RegisterButtonFactory(IButtonFactory factory)
    {
        _buttonFactory = factory;
    }

    public ICharacterFactory GetCharacterFactory<T>() where T : ICharacterSetup
    {
        return _characterFactories.TryGetValue(typeof(T), out var factory) 
            ? factory 
            : null;
    }

    public IButtonFactory GetButtonFactory()
    {
        return _buttonFactory;
    }
}
