using UnityEngine;
using TMPro;
using Game.Models;
using Zenject;

namespace Game.Views
{
    public class ResourcesPanelView : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TMP_Text _goldText;
        [SerializeField] private TMP_Text _gemText;
        
        // Dependencies
        private IResourceManager _resourceManager;

        [Inject]
        public void Construct(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        private void Start()
        {
            _resourceManager.OnResourcesChanged += UpdateUI;
            UpdateUI();
        }

        private void OnDestroy()
        {
            _resourceManager.OnResourcesChanged -= UpdateUI;
        }

        private void UpdateUI()
        {
            _goldText.text = _resourceManager.Gold.ToString();
            _gemText.text = _resourceManager.Gems.ToString();
        }
    }
}