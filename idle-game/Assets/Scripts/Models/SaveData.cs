using System;
using System.Collections.Generic;

namespace Game.Models
{
    [Serializable]
    public class SaveData
    {
        public int Gold;
        public int Gems;
        public List<TileSaveData> Tiles = new List<TileSaveData>();
    }

    [Serializable]
    public class TileSaveData
    {
        public int X;
        public int Y;

        public string BuildingName;
    }
}