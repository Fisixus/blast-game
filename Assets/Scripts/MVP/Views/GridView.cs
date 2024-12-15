using System;
using Core.GridElements.GridPawns;
using MVP.Views.Interface;
using Unity.Mathematics;
using UnityEngine;

namespace MVP.Views
{
    public class GridView : MonoBehaviour,IGridView
    {
        [field: SerializeField] public SpriteRenderer GridSprite { get; private set; }
        [field: SerializeField] public Transform GridTopLeftTr { get; private set; }
        [field: SerializeField] public Vector2 CellSize { get; private set; }
        [field: SerializeField] public Vector2 GridTopLeftMargin { get; private set; }
        [field: SerializeField] public Vector2 GridPadding { get; private set; }

        private Camera _camera;
        private void Awake()
        {
            _camera = Camera.main;
        }

        public void CalculateGridSize(Vector2Int gridSize)
        {
            var cellHeight = CellSize.y;
            var cellWidth = CellSize.x;

            // Calculate scaled padding based on grid size
            var paddingFactorX = 1f / gridSize.y;
            var paddingFactorY = 1f / gridSize.x;

            var basePadding = GridPadding;
            var scaledPadding = new Vector2(basePadding.x * paddingFactorX, basePadding.y * paddingFactorY);
            GridSprite.size = new Vector2((cellWidth + scaledPadding.x) * gridSize.y,
                (cellHeight + scaledPadding.y) * gridSize.x);
            Debug.Log(GridSprite.size);
            UpdateGridTopLeftTr();
            //CalculateGridTopLeftLocal();
        }
        
        private void CalculateGridTopLeftLocal()
        {
            var localTopLeft = new Vector3(-GridSprite.size.x / 2f, GridSprite.size.y / 2f, 0f); // Local space top-left
            Debug.Log(localTopLeft);
            Matrix4x4 localToWorld = GridSprite.transform.localToWorldMatrix;
            Vector3 worldTopLeft = localToWorld.MultiplyPoint3x4(localTopLeft);
            Debug.Log(worldTopLeft);
            GridTopLeftTr.position = worldTopLeft + (Vector3)GridTopLeftMargin;
        
            //Debug.Log($"Top-Left Position (Local): {GridTopLeftTr.position}");
        }

        public void ScaleGrid()
        {
            var ratio = _camera.aspect;
            var refRatio = math.remap(0.45f, 0.75f, 0.56f, 0.8f, ratio);
            //const float refRatio = 9f / 12f;
            var newScale = ratio / refRatio;
            //cam.orthographicSize = remap ratio;
            GridSprite.transform.localScale = new Vector3(newScale, newScale, 1f);
        }
        
        private void UpdateGridTopLeftTr()
        {
            var bounds = GridSprite.bounds;
            GridTopLeftTr.position = new Vector2(bounds.min.x, bounds.max.y) + GridTopLeftMargin;
            Debug.Log(GridTopLeftTr.position);
        }

    }
}
