// maebleme2

using Zenject;

namespace Ebleme.DI
{
    public class PlayerInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromComponentOnRoot().AsSingle();
            Container.Bind<CameraRotator>().FromComponentOnRoot().AsSingle();
            Container.Bind<Player>().FromComponentOnRoot().AsSingle();
        }
    }
}