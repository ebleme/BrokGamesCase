// maebleme2

using Ebleme.UI;
using UnityEngine;
using Zenject;

namespace Ebleme.DI
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Player playerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerChoosePanel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerUpgradePanel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerSpawner>().FromComponentInHierarchy().AsSingle();

            Container.Bind<IPlayerProvider>()
                .To<PlayerProvider>()
                .AsSingle();
            
            Container.Bind<IPlayerFactory>()
                .To<PlayerFactory>()
                .AsSingle();
        }
    }
}