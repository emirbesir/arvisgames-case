using UnityEngine;
using System.IO;

namespace Game.Models
{
    public class SaveSystem
    {
        private const string SAVE_FILE_NAME = "save.json";
        
        private string SaveFilePath => Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

        public void SaveGame(IResourceManager resources, IGridMap grid)
        {
            SaveData data = new SaveData();
            data.Gold = resources.Gold;
            data.Gems = resources.Gems;
            
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var tile = grid.GetTile(x, y);
                    if (tile.IsOccupied)
                    {
                        data.Tiles.Add(new TileSaveData
                        {
                            X = x,
                            Y = y,
                            BuildingName = tile.OccupyingBuildingName
                        });
                    }
                }
            }

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SaveFilePath, json);
        }

        public SaveData LoadGame()
        {
            if (!File.Exists(SaveFilePath)) return null;

            string json = File.ReadAllText(SaveFilePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        public void DeleteSave()
        {
            if (File.Exists(SaveFilePath))
            {
                File.Delete(SaveFilePath);
            }
        }
    }
}