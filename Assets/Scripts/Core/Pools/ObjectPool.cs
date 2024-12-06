using System.Collections.Generic;
using Core.Interface;
using UnityEngine;

namespace Core.Pools
{
    public class ObjectPool<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour
    {
        private Queue<T> Pool { get; set; }

        public void Initialize(T prefab, Transform parent, int poolSize = 64)
        {
            Pool = new Queue<T>(poolSize);
            for (var i = 0; i < poolSize; i++)
            {
                var obj = Instantiate(prefab, parent);
                Pool.Enqueue(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public T Get()
        {
            if (Pool.Count > 0)
            {
                var obj = Pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }

            return null;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            Pool.Enqueue(obj);
        }
    }
}