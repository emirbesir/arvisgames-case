using UnityEngine;
using Game.Models;
using Zenject;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        private IResourceManager _resourceManager;
        private IGridMap _gridMap;

        [Inject]
        public void Construct(IResourceManager resourceManager, IGridMap gridMap)
        {
            _resourceManager = resourceManager;
            _gridMap = gridMap;
        }

        private void Start()
        {
            // TODO: Load System trigger
        }

        private void OnApplicationQuit()
        {
            // TODO: Save System trigger
        }
    }
}