namespace Utility
{
    public abstract class AbstractSingleton<T> where T : AbstractSingleton<T>, new()
    {
        public static T Instance { get; } = new();
    }
}
