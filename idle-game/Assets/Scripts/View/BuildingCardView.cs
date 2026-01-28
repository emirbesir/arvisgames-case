using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Config;
using Game.Models;

namespace Game.Views
{
    public class BuildingCardView : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image _buildingIconImage;
        [SerializeField] private TMP_Text _buildingNameText;
        [SerializeField] private TMP_Text _goldText;
        [SerializeField] private TMP_Text _gemText;
        
        // Internal references
        private Button _button;
        private CanvasGroup _canvasGroup;
        
        private BuildingDataSO _config;
        private IResourceManager _resourceManager;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public void Setup(BuildingDataSO config, IResourceManager resourceManager)
        {
            _config = config;
            _resourceManager = resourceManager;
            
            _resourceManager.OnResourcesChanged += UpdateState;
            
            _buildingIconImage.sprite = config.BuildingIcon;
            _buildingNameText.text = config.BuildingName;
            _goldText.text = config.GoldCost.ToString();
            _gemText.text = config.GemCost.ToString();
            
            UpdateState();
        }

        private void OnDestroy()
        {
            if (_resourceManager != null)
            {
                _resourceManager.OnResourcesChanged -= UpdateState;
            }
        }

        private void UpdateState()
        {
            if (_config == null) return;

            bool canAfford = _resourceManager.CanAfford(_config.GoldCost, _config.GemCost);

            // Visual feedback for affordability
            _button.interactable = canAfford;
            _canvasGroup.alpha = canAfford ? 1.0f : 0.5f;
        }
        
        public BuildingDataSO GetConfig()
        {
            return _config;
        }
    }
}