using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    public class BonusSpawner : Spawner
    {
        private BonusItemSettings _settings;
        private MainBonusFactory _bonusFactory;

        [Inject]
        public void Construct(BonusItemSettings settings, MainBonusFactory bonusFactory)
        {
            _settings = settings;
            _bonusFactory = bonusFactory;
        }

        public bool TrySpawn(Vector3 position, out IBonusItem bonus)
        {
            bonus = Dequeue() as IBonusItem;

            if (bonus == null)
            {
                bonus = _bonusFactory.CreateByProbability(position);
            }

            if (bonus != null)
            {
                ((MonoBehaviour)bonus).transform.position = position + _settings.CrystalBonusSettings.RelativePosition;

                return true;
            }

            return false;
        }

        public void Despawn(IBonusItem bonus)
        {
            Enqueue(bonus as MonoBehaviour);
        }
    }
}
