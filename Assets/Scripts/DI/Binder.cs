using System.Linq;
using UnityEngine;

namespace DI
{
    public static class Binder
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BindDependencies()
        {
            var dependencies = Object.FindObjectsOfType<MonoBehaviour>().OfType<IDependency>().ToArray();

            foreach (var dependency in dependencies)
            {
                dependency.Bind();
            }
        }
    }
}