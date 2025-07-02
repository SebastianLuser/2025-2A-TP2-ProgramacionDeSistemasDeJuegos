using Services;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class FactoryBootstrapper : MonoBehaviour
{
    [SerializeField] private GameObject defaultButtonPrefab;
    
    private static bool _initialized;

    private void Awake()
    {
        if (_initialized) return;
        
        InitializeFactorySystem();
        _initialized = true;
    }

    private void InitializeFactorySystem()
    {
        var registry = new FactoryRegistry();
        
        RegisterCharacterFactories(registry);
        RegisterButtonFactories(registry);
        
        var characterAbstractFactory = new CharacterAbstractFactory(registry, new CharacterFactory());
        var buttonAbstractFactory = new ButtonAbstractFactory(registry, new ButtonFactory(defaultButtonPrefab));

        ServiceLocator.Register<IFactoryRegistry>(registry);
        ServiceLocator.Register<ICharacterAbstractFactory>(characterAbstractFactory);
        ServiceLocator.Register<IButtonAbstractFactory>(buttonAbstractFactory);
    }

    private void RegisterCharacterFactories(IFactoryRegistry registry)
    {
        var defaultFactory = new CharacterFactory();
        registry.RegisterCharacterFactory<ICharacterSetup>(defaultFactory);
        registry.RegisterCharacterFactory<CharacterSetupAsset>(defaultFactory);
    }

    private void RegisterButtonFactories(IFactoryRegistry registry)
    {
        var buttonFactory = new ButtonFactory(defaultButtonPrefab);
        registry.RegisterButtonFactory(buttonFactory);
    }
}