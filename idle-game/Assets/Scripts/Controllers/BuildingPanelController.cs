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
           
            foreach (var config in _buildingConfigs)
            {
                if (config == null) continue;

                BuildingCardView card = Instantiate(_cardPrefab, transform);
                card.Setup(config, _resourceManager);
                card.OnDragRequested += HandleCardDrag;
            }
        }

        private void HandleCardDrag(BuildingDataSO config)
        {
            _inputController.StartDrag(config);
        }
    }
}