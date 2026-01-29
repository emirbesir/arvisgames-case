using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Config;
using Game.Models;
using UnityEngine.EventSystems;

namespace Game.Views
{
    public class BuildingCardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("UI References")] 
        [SerializeField] private TMP_Text _buildingNameText;
        [SerializeField] private Image _buildingIconImage;
        [SerializeField] private TMP_Text _goldText;
        [SerializeField] private TMP_Text _gemText;

        // Config
        private BuildingDataSO _config;
        
        // Internal References
        private Button _button;
        private CanvasGroup _canvasGroup;
        
        // Dependencies
        private ScrollRect _parentScrollRect;
        private IResourceManager _resourceManager;

        // Events
        public event Action<BuildingDataSO> OnDragRequested;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _parentScrollRect = GetComponentInParent<ScrollRect>();
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
            if (_button.interactable) _parentScrollRect.enabled = false;
            OnDragRequested?.Invoke(_config);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_button.interactable) _parentScrollRect.enabled = true;
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