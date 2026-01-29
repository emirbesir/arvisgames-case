using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class BuildingView : MonoBehaviour
    {
        [Header("Progress Bar")]
        [SerializeField] private Image _fillImage;

        public void SetProgress(float normalizedProgress)
        {
            _fillImage.fillAmount = Mathf.Clamp01(normalizedProgress);
        }
    }
}