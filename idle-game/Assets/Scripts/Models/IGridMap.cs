namespace Game.Models
{
    public interface IGridMap
    {
        int Width { get; }
        int Height { get; }
        
        GridTile GetTile(int x, int y);
        bool IsPositionValid(int x, int y);
        bool CanPlaceBuilding(int x, int y);
        bool PlaceBuilding(int x, int y, string buildingName);
        void ResetMap();
    }
}