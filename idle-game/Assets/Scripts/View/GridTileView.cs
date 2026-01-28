using UnityEngine;

namespace Game.Views
{
    public class GridTileView : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Sprite _occupiedSprite;
        [SerializeField] private Sprite _emptySprite;
        
        [Header("References")]
        [SerializeField] private SpriteRenderer _renderer;
        
        // State
        public int X { get; private set; }
        public int Y { get; private set; }

        public void Setup(int x, int y)
        {
            X = x;
            Y = y;
            
            // Set name for easier debugging in hierarchy
            gameObject.name = $"Tile_{x}_{y}";
            
            // Alternating checkerboard pattern
            bool isOffset = (x + y) % 2 == 1;
            _renderer.color = isOffset ? new Color(0.6f, 0.6f, 0.6f) : Color.white;
            
            UpdateSprite(false);
        }
        
        public void UpdateSprite(bool isOccupied)
        {
            _renderer.sprite = isOccupied ? _occupiedSprite : _emptySprite;
        }
    }
}