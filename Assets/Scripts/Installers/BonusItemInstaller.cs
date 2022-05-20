using Zenject;

namespace JM.TestTask
{
    public class BonusItemInstaller : MonoInstaller
    {
        private void BindMainFactory()
        {
            Container
                .Bind<MainBonusFactory>()
                .AsSingle();
        }

        private void BindCrystalFactory()
        {
            Container
                .Bind<IBonusItemFactory>()
                .To<CrystalFactory>()
                .AsSingle();
        }

        public override void InstallBindings()
        {
            BindMainFactory();
            BindCrystalFactory();
        }
    }
}
