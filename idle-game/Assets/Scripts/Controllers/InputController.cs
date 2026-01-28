using UnityEngine;
using Game.Models;
using Game.Views;
using Game.Config;
using Zenject;

namespace Game.Controllers
{
    public class InputController : MonoBehaviour
    {
        // State
        private BuildingDataSO _currentDraggingConfig;
        private bool _isDragging;
        
        // Dependencies
        private IGridMap _gridMap;
        private IResourceManager _resourceManager;
        private GridMapView _gridMapView;
        private Camera _mainCamera;
        
        [Inject]
        public void Construct(IGridMap gridMap, IResourceManager resourceManager, GridMapView gridMapView)
        {
            _gridMap = gridMap;
            _resourceManager = resourceManager;
            _gridMapView = gridMapView;
            _mainCamera = Camera.main;
        }

        public void StartDrag(BuildingDataSO config)
        {
            if (!_resourceManager.CanAfford(config.GoldCost, config.GemCost)) return;

            _currentDraggingConfig = config;
            _isDragging = true;
            
            _gridMapView.CreateGhost(config.BuildingPrefab);
        }

        private void Update()
        {
            if (!_isDragging) return;
            
            if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
                return;
            }
            
            // Update ghost position and color
            GetXYFromMousePosition(out int x, out int y);
            bool canBuild = _gridMap.CanPlaceBuilding(x, y);
            _gridMapView.UpdateGhost(x, y, canBuild);
        }

        private void EndDrag()
        {
            if (!_isDragging) return;
            
            GetXYFromMousePosition(out int x, out int y);

            // Try to place
            if (_gridMap.PlaceBuilding(x, y, _currentDraggingConfig.BuildingName))
            {
                _resourceManager.SpendResources(_currentDraggingConfig.GoldCost, _currentDraggingConfig.GemCost);
                _gridMapView.PlaceBuildingVisual(x, y, _currentDraggingConfig.BuildingPrefab);
            }

            // Cleanup
            _gridMapView.DestroyGhost();
            _isDragging = false;
            _currentDraggingConfig = null;
        }
        
        private void GetXYFromMousePosition(out int x, out int y)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(mousePos);
            Vector3 localPos = _gridMapView.transform.InverseTransformPoint(worldPos);
            float halfWidth = _gridMap.Width / 2.0f;
            float halfHeight = _gridMap.Height / 2.0f;
            x = Mathf.FloorToInt(localPos.x + halfWidth);
            y = Mathf.FloorToInt(localPos.y + halfHeight);
        }
    }
}