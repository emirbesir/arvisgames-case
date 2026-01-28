using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Config;
using Game.Models;
using UnityEngine.EventSystems;

namespace Game.Views
{
    public class BuildingCardView : MonoBehaviour, IPointerDownHandler
    {
        [Header("UI References")] 
        [SerializeField] private TMP_Text _buildingNameText;
        [SerializeField] private Image _buildingIconImage;
        [SerializeField] private TMP_Text _goldText;
        [SerializeField] private TMP_Text _gemText;

        // Config
        private BuildingDataSO _config;
        
        // Dependencies
        private Button _button;
        private CanvasGroup _canvasGroup;
        private IResourceManager _resourceManager;

        // Events
        public event Action<BuildingDataSO> OnDragRequested;

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

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDragRequested?.Invoke(_config);
        }

        private void OnDestroy()
        {
            _resourceManager.OnResourcesChanged -= UpdateState;
        }

        private void UpdateState()
        {
            bool canAfford = _resourceManager.CanAfford(_config.GoldCost, _config.GemCost);
            
            _button.interactable = canAfford;
            _canvasGroup.alpha = canAfford ? 1.0f : 0.5f;
        }
    }
}