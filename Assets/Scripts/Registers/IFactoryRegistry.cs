public interface IFactoryRegistry
{
    void RegisterCharacterFactory<T>(ICharacterFactory factory) where T : ICharacterSetup;
    void RegisterButtonFactory(IButtonFactory factory);
    ICharacterFactory GetCharacterFactory<T>() where T : ICharacterSetup;
    IButtonFactory GetButtonFactory();
}