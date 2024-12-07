using System;
using System.Collections.Generic;

namespace DI
{
    public class Container
    {
        private readonly Dictionary<Type, Func<object>> _bindings = new();

        // Bind a type as a Singleton
        public void BindAsSingle<T>(Func<T> factory) where T : class
        {
            T instance = null;
            _bindings[typeof(T)] = () => instance ??= factory();
        }
        public void BindAsSingleNonLazy<T>(Func<T> factory) where T : class
        {
            BindAsSingle(factory);
            _bindings[typeof(T)].Invoke();
        }
        // Bind a type as Transient
        public void BindAsTransient<T>(Func<T> factory) where T : class
        {
            _bindings[typeof(T)] = () => factory();
        }

        // Resolve a type
        public T Resolve<T>() where T : class
        {
            if (_bindings.TryGetValue(typeof(T), out var factory))
                return factory() as T;

            throw new Exception($"Type {typeof(T).Name} not bound in container.");
        }
    }
}
