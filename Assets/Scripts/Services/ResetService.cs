using Zenject;

namespace JM.TestTask
{
    public class ResetService : IInitializable
    {
        private IMap _map;
        private IPlayer _player;
        private InputService _inputService;
        private ScoreCounter _scoreCounter;

        private void OnMapGenerated()
        {
            _player.Respawn();

            _player.Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _inputService.Click += Reset;
        }

        private void Reset()
        {
            _inputService.Click -= Reset;

            _map.Regenerate();
            _scoreCounter.Reset();
        }

        [Inject]
        public void Construct(IMap map, IPlayer player, InputService inputService, ScoreCounter scoreCounter)
        {
            _map = map;
            _player = player;
            _inputService = inputService;
            _scoreCounter = scoreCounter;
        }

        public void Initialize()
        {
            _map.Generated += OnMapGenerated;

            _map.Generate();
        }
    }
}
