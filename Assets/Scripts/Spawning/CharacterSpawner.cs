using Services;
using Spawning;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ICharacterSpawner
{
    private ICharacterAbstractFactory _characterFactory;

    public void Awake()
    {
        _characterFactory = new CharacterFactory();
        ServiceLocator.Register<ICharacterSpawner>(this); 
    }
    
    private void OnDestroy()
    {
        ServiceLocator.Unregister<ICharacterSpawner>();
    }
    

    public void Spawn(ICharacterSetup config)
    {
        if (_characterFactory == null)
        {
            return;
        }
        
        ((ISetup<ICharacterSetup>)_characterFactory).Setup(config);
        _characterFactory.CreateCharacter(transform.position, transform.rotation);
    }
}