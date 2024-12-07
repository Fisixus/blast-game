using System.Collections.Generic;
using Core.Factories.Interface;
using Core.Pools;
using Core.Pools.Interface;
using UnityEngine;

namespace Core.Factories
{
    public class ObjectFactory<T> : MonoBehaviour, IFactory<T> where T : MonoBehaviour
    {
        [field: SerializeField] public T ObjPrefab { get; private set; }

        [field: SerializeField] public Transform ParentTr { get; private set; }

        public IPool<T> Pool { get; private set; }
        
        public void SetPool(int poolSize)
        {
            Pool = new ObjectPool<T>(ObjPrefab, ParentTr, poolSize);
        }

        public virtual T CreateObj()
        {
            return Pool.Get() ?? Instantiate(ObjPrefab, ParentTr);
        }

        public void DestroyObjs(List<T> emptyObjs)
        {
            foreach (var obj in emptyObjs) DestroyObj(obj);
        }

        public virtual void DestroyObj(T emptyObj)
        {
            Pool.Return(emptyObj);
        }
    }
}