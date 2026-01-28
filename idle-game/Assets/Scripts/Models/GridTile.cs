using System;

namespace Game.Models
{
    [Serializable]
    public class GridTile
    {
        // Position
        public int X { get; private set; }
        public int Y { get; private set; }
        
        // State
        public bool IsOccupied { get; private set; }
        public string OccupyingBuildingName { get; private set; }
        
        public GridTile(int x, int y)
        {
            X = x;
            Y = y;
            IsOccupied = false;
            OccupyingBuildingName = null;
        }

        public void SetOccupied(string buildingName)
        {
            IsOccupied = true;
            OccupyingBuildingName = buildingName;
        }

        public void Clear()
        {
            IsOccupied = false;
            OccupyingBuildingName = null;
        }
    }
}