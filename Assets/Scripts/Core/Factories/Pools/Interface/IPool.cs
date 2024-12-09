namespace Core.Factories.Pools.Interface
{
    public interface IPool<T>
    {
        public T Get();
        public void Return(T obj);
    }
}