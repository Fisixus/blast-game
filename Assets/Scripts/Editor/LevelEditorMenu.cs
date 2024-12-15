using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class LevelEditorMenu
    {
        [MenuItem("Tools/Level Editor/Adjust Current Level")]
        public static void AdjustCurrentLevel()
        {
            Debug.Log("Adjust Current Level clicked");
            // Implement level adjustment logic here
        }
    }
}