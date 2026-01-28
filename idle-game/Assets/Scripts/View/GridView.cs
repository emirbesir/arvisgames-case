using UnityEngine;
using Game.Models;
using Zenject;

namespace Game.Views
{
    public class GridView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GridTileView _tilePrefab;
        [SerializeField] private float _cellSize = 1.0f;

        private IGridMap _gridMap;
        private GridTileView[,] _tileViews;

        [Inject]
        public void Construct(IGridMap gridMap)
        {
            _gridMap = gridMap;
            GenerateGridVisuals();
        }

        private void GenerateGridVisuals()
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            _tileViews = new GridTileView[_gridMap.Width, _gridMap.Height];

            float offsetX = _gridMap.Width * _cellSize / 2.0f - (_cellSize / 2.0f);
            float offsetY = _gridMap.Height * _cellSize / 2.0f - (_cellSize / 2.0f);

            for (int x = 0; x < _gridMap.Width; x++)
            {
                for (int y = 0; y < _gridMap.Height; y++)
                {
                    GridTileView tile = Instantiate(_tilePrefab, transform);
                    
                    float posX = x * _cellSize - offsetX;
                    float posY = y * _cellSize - offsetY;
                    tile.transform.localPosition = new Vector3(posX, posY, 0);

                    tile.Setup(x, y);
                    _tileViews[x, y] = tile;
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            if (_tileViews != null && x >= 0 && x < _gridMap.Width && y >= 0 && y < _gridMap.Height)
            {
                return _tileViews[x, y].transform.position;
            }
            return Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            
            float width = (_gridMap != null ? _gridMap.Width : 10) * _cellSize;
            float height = (_gridMap != null ? _gridMap.Height : 10) * _cellSize;

            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1));
        }
    }
}