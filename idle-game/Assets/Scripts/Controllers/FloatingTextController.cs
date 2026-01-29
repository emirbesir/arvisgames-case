using UnityEngine;
using Game.Views;
using Zenject;

namespace Game.Controllers
{
    public class FloatingTextController : MonoBehaviour
    {
        [SerializeField] private FloatingTextView _textPrefab;
        
        public void SpawnText(string content, Vector3 worldPosition, Color color)
        {
            FloatingTextView instance = Instantiate(_textPrefab, worldPosition, Quaternion.identity);
            instance.Setup(content, color);
        }
    }
}