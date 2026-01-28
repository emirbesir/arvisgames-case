using UnityEngine;

namespace Game.Views
{
    public class GridTileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        
        public int X { get; private set; }
        public int Y { get; private set; }

        public void Setup(int x, int y)
        {
            X = x;
            Y = y;
            
            // Set name for easier debugging in hierarchy
            gameObject.name = $"Tile_{x}_{y}";
            
            // Alternating checkerboard pattern (Visual Polish)
            bool isOffset = (x + y) % 2 == 1;
            _renderer.color = isOffset ? new Color(0.6f, 0.6f, 0.6f) : Color.white;
        }
    }
}