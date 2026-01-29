using Game.Controllers;
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
        private FloatingTextController _floatingTextController;

        [Inject]
        public void Construct(IResourceManager resourceManager, FloatingTextController floatingTextController)
        {
            _resourceManager = resourceManager;
            _floatingTextController = floatingTextController;
        }

        private void Start()
        {
            _resourceManager.OnResourcesChanged += UpdateUI;
            _resourceManager.OnResourcesDelta += InstantiateFloatingTexts;
            UpdateUI();
        }

        private void OnDestroy()
        {
            _resourceManager.OnResourcesChanged -= UpdateUI;
            _resourceManager.OnResourcesDelta -= InstantiateFloatingTexts;
        }

        private void UpdateUI()
        {
            _goldText.text = _resourceManager.Gold.ToString();
            _gemText.text = _resourceManager.Gems.ToString();
        }

        private void InstantiateFloatingTexts(int goldDelta, int gemDelta)
        {
            if (goldDelta != 0)
            {
                string goldText = (goldDelta > 0 ? "+" : "") + goldDelta.ToString() + " Gold";
                Color textColor = goldDelta > 0 ? Color.yellow : Color.red;
                Vector3 textPosition = _goldText.transform.position + Vector3.up * 0.5f;
                _floatingTextController.SpawnText(goldText, textPosition, textColor);
            }

            if (gemDelta != 0)
            {
                string gemText = (gemDelta > 0 ? "+" : "") + gemDelta.ToString() + " Gems";
                Color textColor = gemDelta > 0 ? Color.cyan : Color.red;
                Vector3 textPosition = _gemText.transform.position + Vector3.up * 0.5f;
                _floatingTextController.SpawnText(gemText, textPosition, textColor);
            }
        }
    }
}