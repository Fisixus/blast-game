using UnityEngine;

namespace Core.Interface
{
    public interface IPool<T>
    {
        public T Get();
        public void Initialize(T prefab, Transform parentTr, int poolSize = 64);
        public void Return(T obj);
    }
}