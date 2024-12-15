using Core.LevelSerialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdjustmentWindow : EditorWindow
{
    private int _gridWidth = 10;
    private int _gridHeight = 10;
    private int _moveCount = 10;
    private JsonGridObjectType[,] _gridObjects;

    [MenuItem("Tools/Level Editor/Adjust Current Level")]
    public static void ShowWindow()
    {
        GetWindow<LevelAdjustmentWindow>("Adjust Current Level");
    }

    private void OnEnable()
    {
        // Initialize the grid array if it's null
        if (_gridObjects == null)
        {
            _gridObjects = new JsonGridObjectType[_gridHeight, _gridWidth];
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Adjust Current Level", EditorStyles.boldLabel);

        // Get new grid dimensions
        int newGridWidth = EditorGUILayout.IntField("Grid Width", _gridWidth);
        int newGridHeight = EditorGUILayout.IntField("Grid Height", _gridHeight);

        _moveCount = EditorGUILayout.IntField("Move Count", _moveCount);
        EditorGUILayout.LabelField("Items Table", EditorStyles.boldLabel);

        // Resize grid array if dimensions change
        if (newGridWidth != _gridWidth || newGridHeight != _gridHeight)
        {
            ResizeGrid(newGridHeight, newGridWidth);
            _gridWidth = newGridWidth;
            _gridHeight = newGridHeight;
        }

        // Render grid and allow enum selection
        for (int i = 0; i < _gridHeight; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < _gridWidth; j++)
            {
                _gridObjects[i, j] = (JsonGridObjectType)EditorGUILayout.EnumPopup(_gridObjects[i, j]);
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Apply Changes"))
        {
            var levelJson = LevelSerializer.ConvertToLevelJson(_gridWidth, _gridHeight, _moveCount, _gridObjects);
            Debug.Log($"Level JSON created:\n{JsonUtility.ToJson(levelJson, true)}");
            AdjustLevel(_gridWidth, _gridHeight);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(this);
        }
    }

    private void ResizeGrid(int newHeight, int newWidth)
    {
        var newGrid = new JsonGridObjectType[newHeight, newWidth];

        // Copy existing values to the new grid within bounds
        for (int i = 0; i < Mathf.Min(_gridHeight, newHeight); i++)
        {
            for (int j = 0; j < Mathf.Min(_gridWidth, newWidth); j++)
            {
                newGrid[i, j] = _gridObjects[i, j];
            }
        }

        _gridObjects = newGrid;
    }

    private static void AdjustLevel(int width, int height)
    {
        var activeScene = SceneManager.GetActiveScene().name;
        if (!activeScene.Equals("LevelScene"))
        {
            Debug.Log("You are not in the level scene!");
        }
    }
}
