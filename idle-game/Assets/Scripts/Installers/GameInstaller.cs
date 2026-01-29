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
            // Models
            Container.Bind<IResourceManager>().To<ResourceManager>().AsSingle();
            Container.Bind<IGridMap>().To<GridMap>().AsSingle();
            
            // Views
            Container.Bind<GridMapView>().FromComponentInHierarchy().AsSingle();
            
            // Controllers
            Container.Bind<InputController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<FloatingTextController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BuildingPanelController>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
        }
    }
}