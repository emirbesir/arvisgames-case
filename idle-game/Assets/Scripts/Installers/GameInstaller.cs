using UnityEngine;
using Zenject;
using Game.Models;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IResourceManager>().To<ResourceManager>().AsSingle();
            Container.Bind<IGridMap>().To<GridMap>().AsSingle();
        }
    }
}