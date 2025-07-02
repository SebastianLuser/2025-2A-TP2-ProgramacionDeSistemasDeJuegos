public interface IButtonAbstractFactory
{
    IButtonFactory GetFactory(object config);
}