using Zenject;

namespace JM.TestTask
{
    public class PlayerInstaller : MonoInstaller
    {
        private BallPlayerSettings _settings;

        [Inject]
        public void Construct(BallPlayerSettings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container
                .Bind<IPlayer>()
                .To<BallPlayer>()
                .FromComponentInNewPrefab(_settings.PlayerPrefab)
                .AsSingle();
        }
    }
}
