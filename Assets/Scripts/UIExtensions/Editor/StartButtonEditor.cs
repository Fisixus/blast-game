using UnityEditor;

namespace UIExtensions.Editor
{
    [CustomEditor(typeof(StartButton))]
    public class ButtonExtendedEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Draws the default Button inspector
        }
    }
}