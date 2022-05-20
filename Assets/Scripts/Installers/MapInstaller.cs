using Zenject;

namespace JM.TestTask
{
    public class MapInstaller : MonoInstaller
    {
        private CubicMapSettings _settings;

        private void BindTileFactory()
        {
            Container
                .BindFactory<CubicTile, CubicTileFactory>()
                .FromComponentInNewPrefab(_settings.TilePrefab)
                .WithGameObjectName("Tile")
                .UnderTransformGroup("Tiles");
        }

        private void BindMap()
        {
            Container
                .BindInterfacesAndSelfTo<CubicMap>()
                .AsSingle();
        }

        private void BindSpawners()
        {
            Container
                .Bind<TileSpawner>()
                .AsSingle();

            Container
                .Bind<BonusSpawner>()
                .AsSingle();
        }

        [Inject]
        public void Construct(CubicMapSettings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            BindTileFactory();
            BindMap();
            BindSpawners();
        }
    }
}