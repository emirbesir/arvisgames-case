using UnityEngine;
using TMPro;

namespace Game.Views
{
    public class FloatingTextView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private float _fadeDuration = 1.0f;
        
        [Header("References")]
        [SerializeField] private TMP_Text _textComponent;

        private float _timer;
        private Color _startColor;

        public void Setup(string text, Color color)
        {
            _textComponent.text = text;
            _textComponent.color = color;
            _startColor = color;
            _timer = 0;
        }

        private void Update()
        {
            // Move up
            transform.position += Vector3.up * (_moveSpeed * Time.deltaTime);

            // Fade out
            _timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, _timer / _fadeDuration);
            _textComponent.color = new Color(_startColor.r, _startColor.g, _startColor.b, alpha);

            if (_timer >= _fadeDuration)
            {
                Destroy(gameObject);
            }
        }
    }
}