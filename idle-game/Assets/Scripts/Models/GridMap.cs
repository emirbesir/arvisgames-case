namespace Game.Models
{
    public class GridMap : IGridMap
    {
        private const int MAP_WIDTH = 10;
        private const int MAP_HEIGHT = 10;
        
        private GridTile[,] _grid;

        public int Width => MAP_WIDTH;
        public int Height => MAP_HEIGHT;

        public GridMap()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            _grid = new GridTile[MAP_WIDTH, MAP_HEIGHT];
            for (int x = 0; x < MAP_WIDTH; x++)
            {
                for (int y = 0; y < MAP_HEIGHT; y++)
                {
                    _grid[x, y] = new GridTile(x, y);
                }
            }
        }

        public void ResetMap()
        {
            InitializeGrid();
        }

        public GridTile GetTile(int x, int y)
        {
            if (!IsPositionValid(x, y)) return null;
            return _grid[x, y];
        }

        public bool IsPositionValid(int x, int y)
        {
            return x >= 0 && x < MAP_WIDTH && y >= 0 && y < MAP_HEIGHT;
        }

        public bool CanPlaceBuilding(int x, int y)
        {
            if (!IsPositionValid(x, y)) return false;
            return !_grid[x, y].IsOccupied;
        }

        public bool PlaceBuilding(int x, int y, string buildingName)
        {
            if (!CanPlaceBuilding(x, y)) return false;

            _grid[x, y].SetOccupied(buildingName);
            return true;
        }
    }
}