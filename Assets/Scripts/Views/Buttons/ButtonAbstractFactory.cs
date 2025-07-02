public class ButtonAbstractFactory : IButtonAbstractFactory
{
    private readonly IFactoryRegistry _registry;
    private readonly IButtonFactory _defaultFactory;

    public ButtonAbstractFactory(IFactoryRegistry registry, IButtonFactory defaultFactory)
    {
        _registry = registry;
        _defaultFactory = defaultFactory;
    }

    public IButtonFactory GetFactory(object config)
    {
        var factory = _registry.GetButtonFactory() ?? _defaultFactory;
        factory.Setup(config);
        return factory;
    }
}