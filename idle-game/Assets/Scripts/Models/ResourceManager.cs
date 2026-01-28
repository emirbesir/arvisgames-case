using System;

namespace Game.Models
{
    [Serializable]
    public class ResourceManager : IResourceManager
    {
        // Constants
        private const int STARTING_GOLD = 10;
        private const int STARTING_GEMS = 10;
        
        // State
        public int Gold { get; private set; }
        public int Gems { get; private set; }

        // Events
        public event Action OnResourcesChanged;
        
        public ResourceManager()
        {
            Reset();
        }

        public void Reset()
        {
            Gold = STARTING_GOLD;
            Gems = STARTING_GEMS;
            OnResourcesChanged?.Invoke();
        }

        public bool CanAfford(int goldCost, int gemCost)
        {
            return Gold >= goldCost && Gems >= gemCost;
        }

        public void SpendResources(int goldCost, int gemCost)
        {
            if (!CanAfford(goldCost, gemCost)) return;

            Gold -= goldCost;
            Gems -= gemCost;
            OnResourcesChanged?.Invoke();
        }

        public void AddResources(int goldAmount, int gemAmount)
        {
            Gold += goldAmount;
            Gems += gemAmount;
            OnResourcesChanged?.Invoke();
        }
    }
}