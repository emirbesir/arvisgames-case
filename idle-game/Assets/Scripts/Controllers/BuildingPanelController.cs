using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game.Config;
using Game.Views;
using Game.Models;

namespace Game.Controllers
{
    public class BuildingPanelController : MonoBehaviour
    {
        [Header("Buildings")]
        [SerializeField] private List<BuildingDataSO> _buildingConfigs;

        [Header("Building Card")]
        [SerializeField] private BuildingCardView _cardPrefab;
        
        // Dependencies
        private IResourceManager _resourceManager;
        private InputController _inputController;
        
        // Track cards for cleanup
        private List<BuildingCardView> _cards = new List<BuildingCardView>();
        
        [Inject]
        public void Construct(IResourceManager resourceManager, InputController inputController)
        {
            _resourceManager = resourceManager;
            _inputController = inputController;
            GenerateCards();
        }

        private void GenerateCards()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            _cards.Clear();
           
            foreach (var config in _buildingConfigs)
            {
                if (config == null) continue;

                BuildingCardView card = Instantiate(_cardPrefab, transform);
                card.Setup(config, _resourceManager);
                card.OnDragRequested += HandleCardDrag;
                _cards.Add(card);
            }
        }

        private void OnDestroy()
        {
            foreach (var card in _cards)
            {
                if (card != null)
                {
                    card.OnDragRequested -= HandleCardDrag;
                }
            }
            _cards.Clear();
        }

        private void HandleCardDrag(BuildingDataSO config)
        {
            _inputController.StartDrag(config);
        }
        
        public BuildingDataSO GetConfigByName(string name)
        {
            foreach (var config in _buildingConfigs)
            {
                if (config.BuildingName == name)
                {
                    return config;
                }
            }
            return null;
        }
    }
}