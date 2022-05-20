using UnityEngine.UI;
using Zenject;

namespace JM.TestTask
{
    public class ScoreCounter : IInitializable
    {
        ScoreCounterSettings _settings;
        private IPlayer _player; 
        private Text _renderer;
        private int _score;

        private void IncreaseScore(int score)
        {
            _score += score;

            _renderer.text = _score.ToString();
        }

        [Inject]
        public void Construct(ScoreCounterSettings settings, IPlayer player, Text renderer)
        {
            _player = player;
            _renderer = renderer;
            _settings = settings;
        }

        public void Initialize()
        {
            _score = -1;

            _player.TileActivated += () => IncreaseScore(_settings.TilePrice);
            _player.BonusCollected += () => IncreaseScore(_settings.BonusPrice);
        }

        public void Reset()
        {
            _score = -1;
            _renderer.text = string.Empty;
        }
    }
}
