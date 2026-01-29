using System;

namespace Game.Models
{
    public interface IResourceManager
    {
        int Gold { get; }
        int Gems { get; }
        
        event Action OnResourcesChanged;
        event Action<int, int> OnResourcesDelta;
        
        bool CanAfford(int goldCost, int gemCost);
        void SpendResources(int goldCost, int gemCost);
        void AddResources(int goldAmount, int gemAmount);
        void Reset();
        void LoadState(int gold, int gems);
    }
}