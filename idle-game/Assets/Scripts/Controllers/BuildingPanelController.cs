using System.Collections.Generic;
using UnityEngine;
using Game.Config;
using Game.Models;
using Game.Views;
using Zenject;

namespace Game.Controllers
{
    public class BuildingPanelController : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private List<BuildingDataSO> _buildingConfigs;

        [Header("References")]
        [SerializeField] private BuildingCardView _cardPrefab;

        private IResourceManager _resourceManager;

        [Inject]
        public void Construct(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            
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
            }
        }
    }
}