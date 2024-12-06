using System;
using System.Collections.Generic;

namespace DI
{
    /// <summary>
    /// Simple Dependency Injector for general use, allowing one dependency per type.
    /// </summary>
    public static class DI
    {
        private static readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>(32);

        public static void Bind<T>(T obj)
        {
            var type = typeof(T);
            
            if (_dictionary.ContainsKey(type))
                _dictionary[type] = obj;
            else
                _dictionary.Add(type, obj);
        }

        public static T Resolve<T>()
        {
            var type = typeof(T);

            if (!_dictionary.ContainsKey(type))
                throw new Exception($"No {type} reference in container.");

            return (T)_dictionary[type];
        }
    }
}