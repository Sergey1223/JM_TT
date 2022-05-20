using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    [CreateAssetMenu(fileName = "NewGameSettings", menuName = "GameSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public CubicMapSettings mapSettings;
        public BallPlayerSettings playerSettings;
        public BonusItemSettings bonusItemSettings;
        public ScoreCounterSettings scoreCounterSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(mapSettings);
            Container.BindInstance(playerSettings);
            Container.BindInstance(bonusItemSettings);
            Container.BindInstance(scoreCounterSettings);
        }
    }
}
