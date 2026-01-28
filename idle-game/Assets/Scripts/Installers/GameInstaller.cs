using Zenject;
using Game.Views;
using Game.Models;
using Game.Controllers;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IResourceManager>().To<ResourceManager>().AsSingle();
            Container.Bind<IGridMap>().To<GridMap>().AsSingle();
            Container.Bind<GridMapView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InputController>().FromComponentInHierarchy().AsSingle();
        }
    }
}