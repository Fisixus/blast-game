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
        private Vector3 _gridDefaultScale;
        private void Awake()
        {
            _camera = Camera.main;
            _gridDefaultScale = GridSprite.transform.localScale;
        }

        public void CalculateGridSize(Vector2Int gridSize)
        {
            ResetGridScale();
            var cellHeight = CellSize.y;
            var cellWidth = CellSize.x;

            // Calculate scaled padding based on grid size
            var paddingFactorX = 1f / gridSize.y;
            var paddingFactorY = 1f / gridSize.x;

            var basePadding = GridPadding;
            var scaledPadding = new Vector2(basePadding.x * paddingFactorX, basePadding.y * paddingFactorY);
            GridSprite.size = new Vector2((cellWidth + scaledPadding.x) * gridSize.y,
                (cellHeight + scaledPadding.y) * gridSize.x);
            UpdateGridTopLeftTr();
        }

        private void ResetGridScale()
        {
            GridSprite.transform.localScale = _gridDefaultScale;
        }
        
        public void ScaleGrid()
        {
            ResetGridScale();
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
        }

    }
}
