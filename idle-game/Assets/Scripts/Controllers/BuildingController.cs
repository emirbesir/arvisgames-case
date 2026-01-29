using UnityEngine;
using Game.Models;
using Game.Config;
using Game.Views;
using Zenject;

namespace Game.Controllers
{
    [RequireComponent(typeof(BuildingView))]
    public class BuildingController : MonoBehaviour
    {
        // State
        private BuildingDataSO _config;
        private float _productionTimer;
        private bool _isActive;
        
        // Dependencies
        private BuildingView _view;
        private IResourceManager _resourceManager;
        private FloatingTextController _textController;
        
        [Inject]
        public void Construct(IResourceManager resourceManager, FloatingTextController textController)
        {
            _resourceManager = resourceManager;
            _textController = textController;
        }

        public void Initialize(BuildingDataSO config)
        {
            _config = config;
            _view = GetComponent<BuildingView>();
            _isActive = true;
            _productionTimer = 0f;
        }

        private void Update()
        {
            if (!_isActive) return;
            
            _productionTimer += Time.deltaTime;
            
            if (_config.ProductionDuration > 0)
            {
                float progress = _productionTimer / _config.ProductionDuration;
                _view.SetProgress(progress);
                
                if (_productionTimer >= _config.ProductionDuration)
                {
                    CollectResources();
                    _productionTimer = 0f;
                }
            }
        }

        private void CollectResources()
        {
            _resourceManager.AddResources(_config.GoldOutput, _config.GemOutput);
            
            Vector3 textPosition = transform.position + Vector3.up * 0.5f;
            
            if (_config.GoldOutput > 0)
            {
                _textController.SpawnText($"+{_config.GoldOutput} Gold", textPosition, Color.yellow);
            }
            
            if (_config.GemOutput > 0)
            {
                _textController.SpawnText($"+{_config.GemOutput} Gem", textPosition, Color.cyan);
            }
        }
    }
}