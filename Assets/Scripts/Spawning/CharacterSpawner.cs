using Services;
using Spawning;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ICharacterSpawner
{
    private ICharacterAbstractFactory _characterAbstractFactory;

    public void Awake()
    {
        ServiceLocator.Register<ICharacterSpawner>(this); 
    }

    private void Start()
    {
        _characterAbstractFactory = ServiceLocator.Get<ICharacterAbstractFactory>();
    }
    
    private void OnDestroy()
    {
        ServiceLocator.Unregister<ICharacterSpawner>();
    }

    public void Spawn(ICharacterSetup config)
    {
        if (_characterAbstractFactory == null || config == null) return;
        
        var factory = _characterAbstractFactory.GetFactory(config);
        factory.CreateCharacter(transform.position, transform.rotation);
    }
}