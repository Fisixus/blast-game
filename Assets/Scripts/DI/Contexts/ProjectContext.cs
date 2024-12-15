using UnityEngine;

namespace DI.Contexts
{
    public class ProjectContext : MonoBehaviour
    {
        public static Container Container { get; private set; } = new();

        [SerializeField] private Installer[] _installers;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            // Load ProjectContext prefab
            var prefab = Resources.Load<GameObject>("Contexts/ProjectContext");
            if (prefab == null)
            {
                Debug.LogError("Failed to load ProjectContext prefab from Resources/Contexts!");
                return;
            }

            var instance = Object.Instantiate(prefab);
            DontDestroyOnLoad(instance); // Ensure ProjectContext persists across scenes
        }

        private void Awake()
        {
            foreach (var installer in _installers)
            {
                installer.Install(Container);
            }
        }
    }
}