using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class BuildingView : MonoBehaviour
    {
        [Header("Progress Bar")]
        [SerializeField] private Image _fillImage;
        [SerializeField] private TMP_Text _timerText;

        public void SetProgress(float normalizedProgress, float remainingTime)
        {
            _fillImage.fillAmount = Mathf.Clamp01(normalizedProgress);
            
            _timerText.text = remainingTime.ToString("0.0") + "s";
            
        }
    }
}