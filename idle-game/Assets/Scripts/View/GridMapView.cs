using Game.Config;
using UnityEngine;
using Game.Models;
using Game.Controllers;
using Zenject;

namespace Game.Views
{
    public class GridMapView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GridTileView _tilePrefab;
        [SerializeField] private float _cellSize = 1.0f;
        [Header("Ghost Settings")]
        [SerializeField] private float _ghostAlpha = 0.5f;
        [SerializeField] private Color _validGhostColor = new Color(0, 1, 0, 0.6f);
        [SerializeField] private Color _invalidGhostColor = new Color(1, 0, 0, 0.6f);
        
        // State
        private GameObject _currentGhost;
        private SpriteRenderer _ghostRenderer;
        private GridTileView[,] _tileViews;
        
        // Dependencies
        private IGridMap _gridMap;
        private Camera _mainCamera;
        
        private DiContainer _container;
        
        [Inject]
        public void Construct(IGridMap gridMap, DiContainer container)
        {
            _gridMap = gridMap;
            _container = container;
            _mainCamera = Camera.main;
            
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
        
        public void CreateGhost(GameObject prefab)
        {
            if (_currentGhost != null) DestroyGhost();

            _currentGhost = Instantiate(prefab, transform);
            
            var logic = _currentGhost.GetComponent<BuildingController>();
            Destroy(logic);
            
            var view = _currentGhost.GetComponent<BuildingView>();
            Destroy(view);
            
            _ghostRenderer = _currentGhost.GetComponentInChildren<SpriteRenderer>();
            
            Color c = _ghostRenderer.color;
            c.a = _ghostAlpha;
            _ghostRenderer.color = c;
        }

        public void UpdateGhost(int x, int y, bool isValid)
        {
            if (_currentGhost == null) return;
            
            // Snap to grid
            if (x >= 0 && x < _gridMap.Width && y >= 0 && y < _gridMap.Height)
            {
                _currentGhost.transform.position = GetWorldPosition(x, y);
            }
            else
            {
                // Follow mouse
                Vector3 worldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 0;
                _currentGhost.transform.position = worldPos;
            }
            
            _ghostRenderer.color = isValid ? _validGhostColor : _invalidGhostColor;
        }

        public void DestroyGhost()
        {
            Destroy(_currentGhost);
            _currentGhost = null;
            _ghostRenderer = null;
        }

        public void PlaceBuildingVisual(int x, int y, BuildingDataSO config)
        {
            GameObject building = _container.InstantiatePrefab(config.BuildingPrefab, transform);
            building.transform.position = GetWorldPosition(x, y);
            
            var controller = building.GetComponent<BuildingController>();
            if (controller != null)
            {
                controller.Initialize(config);
            }
            
            _tileViews[x, y].UpdateSprite(true);
        }
        
        // Gizmos for visualizing grid in editor
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            
            float width = 10 * _cellSize;
            float height = 10 * _cellSize;

            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1));
        }
    }
}