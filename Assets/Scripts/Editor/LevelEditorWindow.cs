using Core.LevelSerialization;
using DI.Contexts;
using MVP.Helpers;
using MVP.Models.Interface;
using MVP.Presenters;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class LevelEditorWindow : EditorWindow
    {
        private int _levelNumber = 0;
        private int _gridWidth = 10;
        private int _gridHeight = 10;
        private int _moveCount = 10;
        private JsonGridObjectType[,] _gridObjects;

        [MenuItem("Tools/Level Editor")]
        public static void ShowWindow()
        {
            GetWindow<LevelEditorWindow>("Level Editor");
        }

        private void OnEnable()
        {
            InitializeGrid();
        }

        private void OnGUI()
        {
            DrawHeader();
            DrawLevelControls();
            DrawGridControls();
            DrawGridTable();
            DrawApplyChangesButton();
        }

        #region Header and Controls

        private void DrawHeader()
        {
            GUILayout.Label("Level Editor", EditorStyles.boldLabel);
        }

        private void DrawLevelControls()
        {
            _levelNumber = EditorGUILayout.IntField("Level Number", _levelNumber);

            if (GUILayout.Button("Load Level"))
            {
                LoadLevel();
            }
        }

        private void DrawGridControls()
        {
            int newGridWidth = EditorGUILayout.IntField("Grid Width", _gridWidth);
            int newGridHeight = EditorGUILayout.IntField("Grid Height", _gridHeight);
            _moveCount = EditorGUILayout.IntField("Move Count", _moveCount);

            if (newGridWidth != _gridWidth || newGridHeight != _gridHeight)
            {
                ResizeGrid(newGridHeight, newGridWidth);
                _gridWidth = newGridWidth;
                _gridHeight = newGridHeight;
            }
        }

        private void DrawGridTable()
        {
            EditorGUILayout.LabelField("Items Table", EditorStyles.boldLabel);

            if (_gridObjects == null)
            {
                Debug.LogWarning("Grid objects not initialized. Initializing with default values.");
                _gridObjects = new JsonGridObjectType[_gridHeight, _gridWidth];
            }

            for (int i = 0; i < _gridHeight; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < _gridWidth; j++)
                {
                    _gridObjects[i, j] = (JsonGridObjectType)EditorGUILayout.EnumPopup(_gridObjects[i, j]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }


        private void DrawApplyChangesButton()
        {
            if (GUILayout.Button("Apply Changes"))
            {
                ApplyChanges();
            }
        }

        #endregion

        #region Level Logic

        private void InitializeGrid()
        {
            if (_gridObjects == null)
            {
                _gridObjects = new JsonGridObjectType[_gridHeight, _gridWidth];
            }
        }

        private void LoadLevel()
        {
            if (!IsLevelSceneActive())
            {
                Debug.LogWarning("You are not in the LevelScene!");
                return;
            }

            var levelModel = ProjectContext.Container.Resolve<ILevelModel>();
            var levelInfo = levelModel.LoadLevel(_levelNumber);

            if (levelInfo == null)
            {
                Debug.LogError($"Failed to load level {_levelNumber}.");
                return;
            }

            // Update grid dimensions and move count
            _gridWidth = levelInfo.GridSize.y;
            _gridHeight = levelInfo.GridSize.x;
            _moveCount = levelInfo.NumberOfMoves;

            // Convert grid data to JsonGridObjectType and update _gridObjects
            var gridData = LevelSerializer.ConvertGridToJsonObjectType(levelInfo.GridObjectTypes);
            ResizeGrid(_gridHeight, _gridWidth);
            PopulateGrid(gridData);

            Debug.Log($"Level {_levelNumber} loaded successfully.");
        }

        private void ApplyChanges()
        {
            var levelJson = LevelSerializer.ConvertToLevelJson(_gridWidth, _gridHeight, _moveCount, _gridObjects);
            var (gridObjectTypes, levelGoals) = LevelSerializer.ProcessLevelJson(levelJson);

            var levelInfo = new LevelInfo(levelJson.level_number, gridObjectTypes, levelGoals, levelJson.move_count);
            Debug.Log($"Level JSON created:\n{JsonUtility.ToJson(levelJson, true)}");

            CreateLevel(levelInfo);
        }

        private static void CreateLevel(LevelInfo levelInfo)
        {
            if (!IsLevelSceneActive())
            {
                Debug.LogWarning("You are not in the LevelScene!");
                return;
            }

            var context = SceneHelper.FindSceneContextInActiveScene();
            var levelPresenter = context.SceneContainer.Resolve<LevelPresenter>();
            levelPresenter.LoadFromLevelEditor(levelInfo);

            Debug.Log("Level created successfully.");
        }

        #endregion

        #region Grid Management

        private void ResizeGrid(int newHeight, int newWidth)
        {
            if (newHeight <= 0 || newWidth <= 0)
            {
                Debug.LogError("Invalid grid dimensions. Height and Width must be greater than 0.");
                return;
            }

            var newGrid = new JsonGridObjectType[newHeight, newWidth];

            if (_gridObjects != null)
            {
                for (int i = 0; i < Mathf.Min(_gridHeight, newHeight); i++)
                {
                    for (int j = 0; j < Mathf.Min(_gridWidth, newWidth); j++)
                    {
                        // Ensure no out-of-bounds access
                        if (i < _gridObjects.GetLength(0) && j < _gridObjects.GetLength(1))
                        {
                            newGrid[i, j] = _gridObjects[i, j];
                        }
                    }
                }
            }

            _gridObjects = newGrid;
            _gridHeight = newHeight;
            _gridWidth = newWidth;
        }



        private void PopulateGrid(JsonGridObjectType[,] gridData)
        {
            for (int i = 0; i < _gridHeight; i++)
            {
                for (int j = 0; j < _gridWidth; j++)
                {
                    _gridObjects[i, j] = gridData[i, j];
                }
            }
        }

        #endregion

        #region Utility Methods

        private static bool IsLevelSceneActive()
        {
            return SceneManager.GetActiveScene().name == "LevelScene";
        }

        #endregion
    }
}
