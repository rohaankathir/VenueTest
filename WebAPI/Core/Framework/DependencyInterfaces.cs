namespace Core.Framework
{
    public interface IDependency
    {
    }

    public interface ISingletonDependency : IDependency
    {
    }

    public interface IPerResquestDependency : IDependency
    {
    }

    public interface IPerLifetimeScope : IDependency
    {
    }

    public interface IPropertyEnable
    {
    }
}
