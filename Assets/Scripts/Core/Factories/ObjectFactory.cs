using System.Collections.Generic;
using Core.Factories.Interface;
using Core.Interface;
using DependencyInjection;
using UnityEngine;

namespace Core.Factories
{
    public class ObjectFactory<T> : MonoBehaviour, IDependency, IFactory<T> where T : MonoBehaviour
    {
        [field: SerializeField] public T ObjPrefab { get; private set; }

        [field: SerializeField] public Transform ParentTr { get; private set; }

        public IPool<T> Pool { get; private set; }
        
        public virtual void Bind()
        {
            DI.Bind(this);
            Pool = DI.Resolve<IPool<T>>();
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