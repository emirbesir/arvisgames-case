using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingDataSO")]
    public class BuildingDataSO : ScriptableObject
    {
        [Header("Display Info")] 
        public string BuildingName;
        public Sprite BuildingIcon;

        [Header("Cost")] 
        public int GoldCost;
        public int GemCost;

        [Header("Production")] 
        public float ProductionDuration;
        public int GoldOutput;
        public int GemOutput;
        
        [Header("Prefab")]
        public GameObject BuildingPrefab;
    }
}