using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    public class CrystalFactory : IBonusItemFactory
    {
        private DiContainer _container;
        private BonusItemSettings _settings;

        public float ProbabilityDensity => _settings.CrystalBonusSettings.ProbabilityDensity;

        public Vector3 RelativePosition => _settings.CrystalBonusSettings.RelativePosition;

        [Inject]
        public void Construct(DiContainer container, BonusItemSettings settings)
        {
            _container = container;
            _settings = settings;
        }

        public IBonusItem Create()
        {
            return _container.InstantiatePrefabForComponent<CrystalBonusItem>(_settings.CrystalBonusSettings.Prefab);
        }
    }
}
