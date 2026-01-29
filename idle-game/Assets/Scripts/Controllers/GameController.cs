using UnityEngine;
using Game.Models;
using Game.Views;
using Zenject;
using UnityEngine.SceneManagement;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        private IResourceManager _resourceManager;
        private IGridMap _gridMap;
        private SaveSystem _saveSystem;
        private BuildingPanelController _buildingPanel;
        private GridMapView _gridView; 

        [Inject]
        public void Construct(IResourceManager resourceManager, IGridMap gridMap, SaveSystem saveSystem, BuildingPanelController buildingPanel, GridMapView gridView)
        {
            _resourceManager = resourceManager;
            _gridMap = gridMap;
            _saveSystem = saveSystem;
            _buildingPanel = buildingPanel;
            _gridView = gridView;
        }

        private void Start()
        {
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private void SaveGame()
        {
            _saveSystem.SaveGame(_resourceManager, _gridMap);
        }

        private void LoadGame()
        {
            SaveData data = _saveSystem.LoadGame();
            if (data == null) return;
            
            // Load resources
            _resourceManager.LoadState(data.Gold, data.Gems);
            
            // Load grid map
            _gridMap.ResetMap();
            foreach (var tileData in data.Tiles)
            {
                _gridMap.PlaceBuilding(tileData.X, tileData.Y, tileData.BuildingName);
                
                var config = _buildingPanel.GetConfigByName(tileData.BuildingName);
                if (config != null)
                {
                    _gridView.PlaceBuildingVisual(tileData.X, tileData.Y, config);
                }
            }
        }

        public void RestartGame()
        {
            _saveSystem.DeleteSave();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}