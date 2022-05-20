using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace JM.TestTask
{
    public class MainBonusFactory : IInitializable
    {
        private IBonusItemFactory[] _factories;
        private BonusItemSettings _settings;
        private float _totalWeight;

        public float SpawnProbability { get => _settings.SpawnProbability; }

        [Inject]
        public void Construct(List<IBonusItemFactory> factories, BonusItemSettings settings)
        {
            _factories = factories.ToArray();
            _settings = settings;
        }

        public IBonusItem CreateByProbability(Vector3 position)
        {
            IBonusItem bonusItem = null;

            float value = Random.value;

            //Debug.Log(value);

            if (_factories.Length > 0 && value < _settings.SpawnProbability)
            {
                IBonusItemFactory factory = null;
                float weight = Random.value * _totalWeight;

                for (int i = 0; i < _factories.Length; i++)
                {
                    if (weight < _factories[i].ProbabilityDensity)
                    {
                        factory = _factories[i];

                        break;
                    }
                    else
                    {
                        weight -= _factories[i].ProbabilityDensity;
                    }
                }

                if (factory == null)
                {
                    factory = _factories[_factories.Length - 1];
                }

                bonusItem = factory.Create();

                if (bonusItem is MonoBehaviour monoBehaviour)
                {
                    monoBehaviour.transform.position = position + factory.RelativePosition;
                }
                else
                {
                    Debug.Log("Bonus item is not a MonoBehaviour.");
                }
            }

            return bonusItem;
        }

        public void Initialize()
        {
            _totalWeight = _factories.Sum((f) => f.ProbabilityDensity);
        }
    }
}
